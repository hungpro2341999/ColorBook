using PaintCraft.Canvas.Configs;
using PaintCraft.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LayerGame : MonoBehaviour
{
	
	private Material material;
	public Color color;
	[SerializeField]
	public ColoringPageConfig PageConfig;
	public float width;
	public float height;
	Camera cam;
	public Vector2 PosInit;
	RenderTexture RenderTexture;
	Texture2D CloneTexure2D;
	Texture2D tmpTexture2D;
	private void Start()
    {
		cam = Camera.main;
		UpdateMeshSize();


	}
	void UpdateMeshSize()
	{
		width = PageConfig.GetSize().x;
		height = PageConfig.GetSize().y;
		MeshFilter mf = GOUtil.CreateComponentIfNoExists<MeshFilter>(gameObject);
		Mesh mesh = MeshUtil.CreatePlaneMesh(width, height);

		mf.mesh = mesh;
		MeshRenderer mr = GOUtil.CreateComponentIfNoExists<MeshRenderer>(gameObject);
		material = new Material(Shader.Find("Unlit/Transparent"));
		CloneTexure2D = new Texture2D((int)PageConfig.GetSize().x,(int)PageConfig.GetSize().y,TextureFormat.RGBA32,false);

		TmpTexture2D = new Texture2D((int)PageConfig.GetSize().x, (int)PageConfig.GetSize().y, TextureFormat.RGBA32, false);

		Graphics.CopyTexture(PageConfig.OutlineTexture, CloneTexure2D);

	//	LoadFromDiskOrClear();
		material.mainTexture = CloneTexure2D;
		mr.material = material;
		var box = gameObject.AddComponent<BoxCollider>();
		
	//	RenderTexture = TextureUtil.SetupRenderTextureOnMaterial(mr.material, PageConfig.GetSize().x, PageConfig.GetSize().y);
	 //    Camera.main.targetTexture = RenderTexture;
	//	SetupTmpTextureSize();
		
	}


	public void SetNewSize()
	{
		//canvas.CanvasCameraController.Camera.targetTexture = null;
		//RenderTexture = TextureUtil.UpdateRenderTextureSize(RenderTexture, canvas.RenderTextureSize.x, canvas.RenderTextureSize.y);
		//GetComponent<MeshRenderer>().material.mainTexture = RenderTexture;
		//canvas.CanvasCameraController.Camera.targetTexture = RenderTexture;
		//MeshUtil.ChangeMeshSize(GetComponent<MeshFilter>().mesh, canvas.RenderTextureSize.x, canvas.RenderTextureSize.y);
	}
	public bool Run = false;
	private void Update()
	{
		
		if (Input.GetMouseButton(0))
		{
			RaycastHit hit;
			if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
				return;
			Vector3 realPos = cam.ScreenToWorldPoint(Input.mousePosition);
			float perX = Mathf.Abs(-GetComponent<BoxCollider>().size.x / 2 - realPos.x) / GetComponent<BoxCollider>().size.x;
			float perY = Mathf.Abs((-GetComponent<BoxCollider>().size.y / 2 - realPos.y) / GetComponent<BoxCollider>().size.y);
			Debug.Log(perX + "  " + perY);
			

			TextureExtension.FloodFillBorder((Texture2D)material.mainTexture, (int)(perX * width), (int)(perY * height), color, new Color(0, 0, 0, 1));
			((Texture2D)material.mainTexture).Apply();
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			SaveTextureAsPNG(CloneTexure2D, SaveFilePath);
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			LoadFromDiskOrClear();
		}




	}
	public Texture2D TmpTexture2D;
	public Texture2D TmpTextureIcon2D;




	public Texture2D load_s01_texture;

	public void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
	{
		load_s01_texture = new Texture2D((int)PageConfig.GetSize().x, (int)PageConfig.GetSize().y, TextureFormat.RGBA32, false);

		File.WriteAllBytes(SaveFilePath, _texture.EncodeToJPG());
	
		
		Debug.Log( "Kb was saved as: " + _fullPath);
	}

	
	void LoadTextureToFile(string filename)
	{
		load_s01_texture = new Texture2D((int)PageConfig.GetSize().x, (int)PageConfig.GetSize().y, TextureFormat.RGBA32, false);
		load_s01_texture.LoadImage(File.ReadAllBytes(filename));
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
			return Path.Combine(SaveDirectory, PageConfig.UniqueId + ".jpg");
		}
	}



	public void SaveChangeStatus()
	{
		
		RenderTexture tmp = RenderTexture.active;
		RenderTexture.active = RenderTexture;

		TmpTexture2D.ReadPixels(new Rect(0, 0, RenderTexture.width,RenderTexture.height), 0, 0, false);

		File.WriteAllBytes(SaveFilePath, TmpTexture2D.EncodeToJPG());

		RenderTexture downscaledRT = RenderTexture.GetTemporary(440, 330);
		Graphics.Blit(Camera.main.targetTexture, downscaledRT);
		RenderTexture.active = downscaledRT;

		TmpTextureIcon2D.ReadPixels(new Rect(0, 0, 440, 330), 0, 0, false);
		File.WriteAllBytes(PageConfig.IconSavePath, TmpTextureIcon2D.EncodeToJPG(100));
		RenderTexture.ReleaseTemporary(downscaledRT);
		RenderTexture.active = tmp;
	}

	public bool LoadFromDiskOrClear()
	{
		if (File.Exists(SaveFilePath) && !string.IsNullOrEmpty(PageConfig.name))
		{
			TmpTexture2D = new Texture2D((int)PageConfig.GetSize().x, (int)PageConfig.GetSize().y, TextureFormat.RGBA32, false);
			if (TmpTexture2D.LoadImage(File.ReadAllBytes(SaveFilePath)))
			{

				CloneTexure2D = new Texture2D(TmpTexture2D.width, TmpTexture2D.height, TextureFormat.RGBA32, false);
				Graphics.CopyTexture(TmpTexture2D, CloneTexure2D);
			
				return true;
			}
		}


		return false;
	}

	
}
