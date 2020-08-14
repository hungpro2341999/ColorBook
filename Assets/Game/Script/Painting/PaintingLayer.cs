﻿using PaintCraft.Canvas.Configs;
using PaintCraft.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class PaintingLayer : MonoBehaviour
{

	public Texture Boreder;
	public TempLayer TemPlayer;
	[SerializeField]
	public CtrlPainting SourcePainting;
	public float width;
	public float height;
	

	Texture2D CloneTexure2D;
	Texture2D tmpTexture2D;
	public Material material;
	public Color ColorPainting;
	public Texture2D TextureReigion;
	public bool load = false;
	public Shader shader;
	public bool StartFloodFill;
	Color[] colorRegion;
	public RenderTexture RenderTexture { get; private set; }

	public string PathSave;
	private Color[] colorTemp;
	private Color[] colorReset;
	public Texture2D textureBorder;
	public void Init()
	{
		//shader = Shader.Find("Mobile/Particles/Additive");
		if (load)
		{

			PathSave = SourcePainting.CacheToPaint.PathSave.path;
			width = SourcePainting.PageConfig.GetSize().x;
			height = SourcePainting.PageConfig.GetSize().y;
			MeshFilter mf = GOUtil.CreateComponentIfNoExists<MeshFilter>(gameObject);
			Mesh mesh = MeshUtil.CreatePlaneMesh(width, height);

			mf.mesh = mesh;
			MeshRenderer mr = GOUtil.CreateComponentIfNoExists<MeshRenderer>(gameObject);
			material = new Material(shader);
			CloneTexure2D = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
			TextureReigion = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);



			Texture2D tex = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false);


			if (tex.LoadImage(File.ReadAllBytes(PathSave)))
			{
				tex.Apply();

			}
			tex = duplicateTexture(tex);

			Graphics.CopyTexture(SourcePainting.PageConfig.OutlineTexture, TextureReigion);
			colorRegion = SourcePainting.PageConfig.OutlineTexture.GetPixels();

			material.mainTexture = tex;
			mr.material = material;
			if (gameObject.GetComponent<BoxCollider>() != null)
			{
				Destroy(gameObject.GetComponent<BoxCollider>());
			}

			gameObject.AddComponent<BoxCollider>();
		
		}
		else
		{

			width = SourcePainting.PageConfig.GetSize().x;
			height = SourcePainting.PageConfig.GetSize().y;
			SourcePainting.T_Complete.text += "GetSize Complete \n";
			MeshFilter mf = GOUtil.CreateComponentIfNoExists<MeshFilter>(gameObject);
			SourcePainting.T_Complete.text += "1 \n";
			Mesh mesh = MeshUtil.CreatePlaneMesh(width, height);
			SourcePainting.T_Complete.text += "2 \n";
			mf.mesh = mesh;
			SourcePainting.T_Complete.text += "3 \n";
			MeshRenderer mr = GOUtil.CreateComponentIfNoExists<MeshRenderer>(gameObject);
			SourcePainting.T_Complete.text += "4 \n";
			material = new Material(shader);
			//Shader.Find("Mobile/Unlit (Supports Lightmap)")
			SourcePainting.T_Complete.text += "Create Plane Complete \n";
			CloneTexure2D = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);

			TextureReigion = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);




			Graphics.CopyTexture(SourcePainting.PageConfig.OutlineTexture, CloneTexure2D);
			SourcePainting.T_Complete.text += "Copy Texture 1 \n";
			Graphics.CopyTexture(SourcePainting.PageConfig.OutlineTexture, TextureReigion);



			SourcePainting.T_Complete.text += "Copy Texture 2 \n";
			material.mainTexture = CloneTexure2D;
			mr.material = material;

			colorRegion = SourcePainting.PageConfig.OutlineTexture.GetPixels();
			if(gameObject.GetComponent<BoxCollider>()!=null)
			{
				Destroy(gameObject.GetComponent<BoxCollider>());
			}
			
			gameObject.AddComponent<BoxCollider>(); 
		

		}
		((Texture2D)material.mainTexture).Apply();
		
	
		TemPlayer.Init();
		colorTemp = ((Texture2D)material.mainTexture).GetPixels();
		for(int i=0;i<colorTemp.Length;i++)
		{
			colorTemp[i] = new Color(0, 0, 0, 0);
		}
		colorReset = colorTemp;
		TemPlayer.SetTempText(colorTemp,Vector3.zero,false);
		textureBorder = new Texture2D((int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height, TextureFormat.ARGB32, false);
		textureBorder = duplicateTexture(textureBorder);
	
	     textureBorder.SetPixels(GetBorder());

		textureBorder.Apply();

		TemPlayer.SetTempText(GetBorder(), Vector3.zero, false);
	}



	public void UpdateLayer()
	{

	}

	public Color[] GetBorder()
	{
		Color[] colors = colorRegion;
		for(int i=0;i<colors.Length;i++)
		{
			if (colors[i] != new Color(0, 0, 0, 1))
			{
				
				colors[i].a = 0;
			}
			
		}
		return colors;

	}

	private void Update()
	{



		if (Input.GetMouseButtonDown(0))
		{
			
			RaycastHit hit;
			if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
				return;
			colorTemp = colorReset;
			TemPlayer.StartFloodFill();
			
			Vector3 realPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float perX = Mathf.Abs(-GetComponent<BoxCollider>().size.x / 2 - realPos.x) / GetComponent<BoxCollider>().size.x;
			float perY = Mathf.Abs((-GetComponent<BoxCollider>().size.y / 2 - realPos.y) / GetComponent<BoxCollider>().size.y);
			Debug.Log(perX + "  " + perY);

			FloodFillBorder((Texture2D)material.mainTexture, colorRegion, (int)(perX * width), (int)(perY * height), ColorPainting, new Color(0, 0, 0, 1), Input.mousePosition);


			TemPlayer.SetTempText(colorTemp, Input.mousePosition,true);

			//((Texture2D)material.mainTexture).Apply();

		}
		if (Input.GetMouseButtonUp(0))
		{
			StartCoroutine(SaveTextureAsPNG((Texture2D)material.mainTexture, PathSave));
		}

		


	}
	public void Apply()
	{
		((Texture2D)material.mainTexture).Apply(); 
	}

	Texture2D duplicateTexture(Texture2D source)
	{

		RenderTexture renderTex = RenderTexture.GetTemporary(
					(int)width, (int)height,

					0,
					RenderTextureFormat.ARGB32,
					RenderTextureReadWrite.Linear);

		Graphics.Blit(source, renderTex);
		RenderTexture previous = RenderTexture.active;
		RenderTexture.active = renderTex;
		Texture2D readableText = new Texture2D((int)width, (int)height);
		readableText.ReadPixels(new Rect(0, 0, (int)width, (int)height), 0, 0);
		readableText.Apply();
		RenderTexture.active = previous;
		RenderTexture.ReleaseTemporary(renderTex);
		return readableText;
	}
	public IEnumerator SaveTextureAsPNG(Texture2D _texture, string _fullPath)
	{


		File.WriteAllBytes(_fullPath,_texture.EncodeToJPG());
		yield return new WaitForSeconds(0);

		Debug.Log("Kb was saved as: " + _fullPath);
	}

	public void SaveToCompleted()
	{
		//FileUtil.DeleteFileOrDirectory(SaveFilePath);
		StartCoroutine(SaveTextureAsPNG((Texture2D)material.mainTexture,SaveSaveCompletedPath));
		CtrlPainting.Ins.ApplyToChangeToCompled();
	}

	public void SaveToShared()
	{
		StartCoroutine(SaveTextureAsPNG((Texture2D)material.mainTexture, SaveSharedPath));
		CtrlPainting.Ins.ApplyToChangeToShared();
	}


	public void TempLayer()
	{

	}

	string SaveDirectory
	{
		get
		{
			string dir = Path.Combine(Application.persistentDataPath, "InProcess");
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			return dir;
		}
	}



	string SaveFilePath
	{
		get
		{
			return Path.Combine(SaveDirectory, SourcePainting.PageConfig.UniqueId + ".jpg");
		}
	}
	string SaveCompleted
	{
		get
		{
			string dir = Path.Combine(Application.persistentDataPath, "Completed");
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			return dir;
		}
	}



	string SaveSaveCompletedPath
	{
		get
		{
			return Path.Combine(SaveCompleted, SourcePainting.PageConfig.UniqueId + ".jpg");
		}
	}
	
	



	string SaveSharedPath
	{
		get
		{
			string dir = Path.Combine(Application.persistentDataPath, "Shared");
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			return Path.Combine(dir, SourcePainting.PageConfig.UniqueId + ".jpg");
		}
	}
	private void OnDisable()
	{
		if (gameObject.GetComponent<BoxCollider>() != null)
		{
			Destroy(gameObject.GetComponent<BoxCollider>());
		}
	}
	
	 public void FloodFillBorder(Texture2D targetTexture, Color[] aTex, int aX, int aY, Color aFillColor, Color aBorderColor, Vector3 mousePosition)
	{
		int w = targetTexture.width;
		int h = targetTexture.height;
		Color[] colors = targetTexture.GetPixels();
		Color[] colorsBorder = aTex;
		byte[] checkedPixels = new byte[colors.Length];
		Color refCol = aBorderColor;
		Queue<Point> nodes = new Queue<Point>();
		nodes.Enqueue(new Point(aX, aY));
		while(nodes.Count > 0)
		{
			Point current = nodes.Dequeue();

			for (int i = current.x; i < w; i++)
			{
				
				if (checkedPixels[i + current.y * w] > 0 || colorsBorder[i + current.y * w] == refCol)
					break;
				colorTemp[i + current.y * w] = aFillColor;
				colors[i + current.y * w] = aFillColor;
				checkedPixels[i + current.y * w] = 1;
				if (current.y + 1 < h)
				{
					if (checkedPixels[i + current.y * w + w] == 0 && colorsBorder[i + current.y * w + w] != refCol)
						nodes.Enqueue(new Point(i, current.y + 1));
				}
				if (current.y - 1 >= 0)
				{
					if (checkedPixels[i + current.y * w - w] == 0 && colorsBorder[i + current.y * w - w] != refCol)
						nodes.Enqueue(new Point(i, current.y - 1));
				}
			}
			for (int i = current.x - 1; i >= 0; i--)
			{
				if (checkedPixels[i + current.y * w] > 0 || colorsBorder[i + current.y * w] == refCol)
					break;
				colorTemp[i + current.y * w] = aFillColor;
				colors[i + current.y * w] = aFillColor;
				checkedPixels[i + current.y * w] = 1;
				if (current.y + 1 < h)
				{
					if (checkedPixels[i + current.y * w + w] == 0 && colorsBorder[i + current.y * w + w] != refCol)
						nodes.Enqueue(new Point(i, current.y + 1));
				}
				if (current.y - 1 >= 0)
				{
					if (checkedPixels[i + current.y * w - w] == 0 && colorsBorder[i + current.y * w - w] != refCol)
						nodes.Enqueue(new Point(i, current.y - 1));
				}

			}

		}
	  
		targetTexture.SetPixels(colors);
	}
	public struct Point
	{
		public short x;
		public short y;
		public Point(short aX, short aY) { x = aX; y = aY; }
		public Point(int aX, int aY) : this((short)aX, (short)aY) { }
	}
}
