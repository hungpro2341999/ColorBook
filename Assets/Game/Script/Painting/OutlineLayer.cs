using PaintCraft.Canvas.Configs;
using PaintCraft.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class OutlineLayer : MonoBehaviour
{
	public Texture2D tex;
	public Material material;
	
	public float height;
	public Shader shader;
	public Transform TransImg;
	public Image Img;
	Camera cam;
	public Vector2 PosInit;
	public SpriteRenderer SpriteImg;

	private void Start()
	{
		SpriteImg = GetComponent<SpriteRenderer>();
	}

	public void SetTexture(Color[] color)
	{
		_textureHolder = new Texture2D((int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height, TextureFormat.RGB24, false); 
		float width = CtrlPainting.Ins.PageConfig.GetSize().x;
		float height = CtrlPainting.Ins.PageConfig.GetSize().y;
		Graphics.CopyTexture(CtrlPainting.Ins.Paint.CloneTexure2D, _textureHolder);
		duplicateTexture(_textureHolder);
		
		_textureHolder.SetPixels(CtrlPainting.Ins.Paint.CloneTexure2D.GetPixels());
		_textureHolder.Apply();
		//	removeTextureBackground();
		//	tex.SetPixels(color);
		tex.Apply();
		GetComponent<SpriteRenderer>().sprite = Sprite.Create(_textureHolder, new Rect(0, 0, (int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height), new Vector2(0.5f, 0.5f));


	

	

	

	}

	public void Init()
	{

		tex = new Texture2D((int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height, TextureFormat.ARGB32, false);
		//tex = duplicateTexture(tex);

		

	}
	Texture2D duplicateTexture(Texture2D source)
	{
		RenderTexture renderTex = RenderTexture.GetTemporary(
					source.width,
					source.height,
					0,
					RenderTextureFormat.Default,
					RenderTextureReadWrite.Linear);

		Graphics.Blit(source, renderTex);
		RenderTexture previous = RenderTexture.active;
		RenderTexture.active = renderTex;
		Texture2D readableText = new Texture2D(source.width, source.height);
		readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
		readableText.Apply();
		RenderTexture.active = previous;
		RenderTexture.ReleaseTemporary(renderTex);
		return readableText;
	}
	public Texture2D _textureHolder;
	public Color _colorHolder = new Color(0,0,0,1);

	void removeTextureBackground()
	{
		if (_textureHolder != null)
		{
			_colorHolder = _textureHolder.GetPixel(0, 0);
			floodFill(0, 0);
			floodFill(0, _textureHolder.height - 1);
			floodFill(_textureHolder.width - 1, 0);
			floodFill(_textureHolder.width - 1, _textureHolder.height - 1);
			_textureHolder.Apply();
		}
	}
	//--------------------------------------------------------------------
	void floodFill(int x, int y)
	{
		if (x < 0 || y < 0 || x > _textureHolder.width || y > _textureHolder.height)
			return;
		Color color = _textureHolder.GetPixel(x, y);
		if (isWhite(color) && color.a > 0)
		{
			color.a = 0;
			_textureHolder.SetPixel(x, y, color);
			floodFill(x - 1, y);
			floodFill(x + 1, y);
			floodFill(x, y - 1);
			floodFill(x, y + 1);
		}
		else
			return;
	}
	//--------------------------------------------------------------
	bool isWhite(Color color)
	{
		float threshold = 1f;
		bool r = Mathf.Abs(color.r - _colorHolder.r) < threshold;
		bool g = Mathf.Abs(color.g - _colorHolder.g) < threshold;
		bool b = Mathf.Abs(color.b - _colorHolder.b) < threshold;
		if (r && g && b)
			return true;
		else
			return false;
	}



}
