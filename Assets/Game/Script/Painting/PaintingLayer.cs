using PaintCraft.Canvas.Configs;
using PaintCraft.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PaintingLayer : MonoBehaviour
{



	[SerializeField]
	public CtrlPainting SourcePainting;
	public float width;
	public float height;
	
	
	Texture2D CloneTexure2D;
	Texture2D tmpTexture2D;
	private Material material;
	public Color ColorPainting;
	public Texture2D TextureReigion;
	public bool load = false;
	public Shader shader;
	 Color[] colorRegion;
	public RenderTexture RenderTexture { get; private set; }
	public void Init()
	{
		//shader = Shader.Find("Mobile/Particles/Additive");
		if (load)
		{
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


			if (tex.LoadImage(File.ReadAllBytes(SaveFilePath)))
			{
				tex.Apply();

			}
			tex = duplicateTexture(tex);

			Graphics.CopyTexture(SourcePainting.PageConfig.OutlineTexture, TextureReigion);


			material.mainTexture = tex;
			mr.material = material;
			var box = gameObject.AddComponent<BoxCollider>();
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
			var box = gameObject.AddComponent<BoxCollider>();
		}
	
	}

	public void UpdateLayer()
	{

	}

	private void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
				return;
			Vector3 realPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float perX = Mathf.Abs(-GetComponent<BoxCollider>().size.x / 2 - realPos.x) / GetComponent<BoxCollider>().size.x;
			float perY = Mathf.Abs((-GetComponent<BoxCollider>().size.y / 2 - realPos.y) / GetComponent<BoxCollider>().size.y);
			Debug.Log(perX + "  " + perY);

			
			
			
			TextureExtension.FloodFillBorder((Texture2D)material.mainTexture,colorRegion,(int)(perX * width), (int)(perY * height), ColorPainting, new Color(0, 0, 0, 1));
			((Texture2D)material.mainTexture).Apply();
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			SaveTextureAsPNG((Texture2D)material.mainTexture, SaveFilePath);
		}




	}
	string SaveDirectory
	{
		get
		{
			string dir = Path.Combine(Application.persistentDataPath, "Saves");
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
	public void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
	{


		File.WriteAllBytes(SaveFilePath, _texture.EncodeToJPG());


		Debug.Log("Kb was saved as: " + _fullPath);
	}
}
