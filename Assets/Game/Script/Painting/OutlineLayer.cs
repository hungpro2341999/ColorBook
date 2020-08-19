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
	

	public void SetTexture(Color[] color)
	{
		float width = CtrlPainting.Ins.PageConfig.GetSize().x;
		float height = CtrlPainting.Ins.PageConfig.GetSize().y;
	
		MeshFilter mf = GOUtil.CreateComponentIfNoExists<MeshFilter>(gameObject);
	
		Mesh mesh = MeshUtil.CreatePlaneMesh(width, height);
	
		mf.mesh = mesh;
	
		MeshRenderer mr = GOUtil.CreateComponentIfNoExists<MeshRenderer>(gameObject);
	
	    material = new Material(shader);

		mr.material = material;
		tex.SetPixels(color);

		tex.Apply();
		mr.material.mainTexture = tex;
		((Texture2D)mr.material.mainTexture).Apply();
	

	}

	public void Init()
	{

		tex = new Texture2D((int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height, TextureFormat.ARGB32, false);
		tex = duplicateTexture(tex);

		

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

	
}
