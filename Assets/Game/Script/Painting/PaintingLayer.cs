using PaintCraft.Canvas.Configs;
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
	public OutlineLayer outline;
	[SerializeField]
	public CtrlPainting SourcePainting;
	public float width;
	public float height;
	

	public Texture2D CloneTexure2D;
	
	public Material material;
	public Color ColorPainting;
	public Texture2D TextureReigion;
	public bool load = false;
	public Shader shader;
	public bool StartFloodFill;
	Color[] colorRegion;
	public float freetime = 0.5f;
	public bool loading  = false;
	public RenderTexture RenderTexture { get; private set; }

	public string PathSave;
	private Color[] colorTemp;
	[HideInInspector]
    public  Color[] colorReset;
	public Texture2D textureBorder;
	public Vector2 TopPixel = new Vector2(9999, 0), BottomPixel = new Vector2(0,99999);

	public Vector3 PosInit;

	public bool StartPaint = false;
	public SpriteRenderer SpriteImg;
	public bool changeLayer = false;
	private byte[] colorBorder;
	public static  float max;
	private void Start()
	{
		SpriteImg = GetComponent<SpriteRenderer>();
		loading = false;
	}
	public void Init()
	{
		loading = false;
	
		if (load)
		{

			PathSave = SourcePainting.CacheToPaint.PathSave.path;
			width = SourcePainting.PageConfig.GetSize().x;
			height = SourcePainting.PageConfig.GetSize().y;
		
			CloneTexure2D = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
			TextureReigion = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);



			


			if (CloneTexure2D.LoadImage(File.ReadAllBytes(PathSave)))
			{
				CloneTexure2D.Apply();

			}
		//	CloneTexure2D = duplicateTexture(CloneTexure2D);

			Graphics.CopyTexture(SourcePainting.PageConfig.OutlineTexture, TextureReigion);
			colorRegion = SourcePainting.PageConfig.OutlineTexture.GetPixels();

			GetComponent<SpriteRenderer>().sprite = Sprite.Create(CloneTexure2D, new Rect(0, 0, (int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height), new Vector2(0.5f, 0.5f));

			if (gameObject.GetComponent<BoxCollider>() != null)
			{
				Destroy(gameObject.GetComponent<BoxCollider>());
			}


			gameObject.AddComponent<BoxCollider>();
			outline.Init();
			outline.SetTexture(GetBorder());

		}
		else
		{

			width = SourcePainting.PageConfig.GetSize().x;
			height = SourcePainting.PageConfig.GetSize().y;
		
			SourcePainting.T_Complete.text += "Create Plane Complete \n";
			CloneTexure2D = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);

			TextureReigion = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);


			Color[] color = CloneTexure2D.GetPixels();
			for(int i=0;i<color.Length;i++)
			{
				color[i] = new Color(1, 1, 1, 1);
			}
			CloneTexure2D.SetPixels(color);
			CloneTexure2D.Apply();

			Graphics.CopyTexture(SourcePainting.PageConfig.OutlineTexture, CloneTexure2D);

			GetComponent<SpriteRenderer>().sprite = Sprite.Create(CloneTexure2D, new Rect(0f, 0f, CloneTexure2D.width, CloneTexure2D.height), new Vector2(0.5f, 0.5f), 100F, 0, SpriteMeshType.FullRect);

			colorRegion = SourcePainting.PageConfig.OutlineTexture.GetPixels();
			if(gameObject.GetComponent<BoxCollider>()!=null)
			{
				Destroy(gameObject.GetComponent<BoxCollider>());
			}
			
			gameObject.AddComponent<BoxCollider>();
			outline.Init();
			outline.SetTexture(GetBorder());

		}
	  
		
	
		TemPlayer.Init();
		GetColorRegion(colorRegion);
	
	//	TemPlayer.SetTempText(CloneTexure2D.GetPixels(), Vector3.zero, false,(changeLayer = !changeLayer));
		StartPaint = false;
	}

	public void GetColorRegion(Color[] color)
	{
		colorBorder = new byte[color.Length];
		for(int i=0;i<color.Length;i++)
		{
	             if(color[i] == new Color(0, 0, 0, 1))
			{
				colorBorder[i] = 1;
			}
			else
			{
				colorBorder[i] = 0;
			}
			
		}
	}

	public void UpdateLayer()
	{

	}

	public Color[] GetBorder()
	{
		Color[] colors = colorRegion;
		for(int i=0;i<colors.Length;i++)
		{
		//	if (Vector4.Distance(new Vector4(colors[i].a, colors[i].r, colors[i].g, colors[i].b), new Vector4(1, 0, 0, 0)) >= 0.01f)
				if (colors[i] != new Color(0, 0, 0, 1))
			{
				
				colors[i].a = 0;
			}
			
		}
		return colors;

	}
	bool isWhite(Color color)
	{
		float threshold = 1f;
		Color _colorHolder = new Color(0, 0, 0, 1);
		bool r = Mathf.Abs(color.r - _colorHolder.r) < threshold;
		bool g = Mathf.Abs(color.g - _colorHolder.g) < threshold;
		bool b = Mathf.Abs(color.b - _colorHolder.b) < threshold;
		if (r && g && b)
			return true;
		else
			return false;
	}


	private void Update()
	{
		if(loading)
		{
			if (freetime < 1f)
			{
				freetime += Time.deltaTime;
			}
			else
			{
				loading = false;
			//	StartCoroutine(SaveTextureAsPNG((Texture2D)material.mainTexture, PathSave));

			}
		}

		

		if(GameManager.Ins.isLoading || GameManager.Ins.isGamePause)
		{
			return;
		}
		if(IsClickUI.IsClick)
		{
			return;
		}
		

		if (!TemPlayer.DoneFloodFill)
			return;

		if (Input.GetMouseButtonDown(0))
		{
			PosInit = Input.mousePosition;
			StartPaint = true;

		}

		if (StartPaint)
		{
			int touch = Input.touchCount;
			if (touch > 1)
			{
				StartPaint = false;
				return; 
			}
		
				
		}
		
		if (Input.GetMouseButtonUp(0))
		{

			if (!StartPaint)
			{
				return;
			}
			StartPaint = false;
			RaycastHit hit;
			if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
				return;
		   
			
			
			Vector3 realPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float perX = Mathf.Abs(-GetComponent<BoxCollider>().size.x / 2 - realPos.x) / GetComponent<BoxCollider>().size.x;
			float perY = Mathf.Abs((-GetComponent<BoxCollider>().size.y / 2 - realPos.y) / GetComponent<BoxCollider>().size.y);
		//	Debug.Log(perX + "  " + perY);

			Color colorPaint = (CloneTexure2D).GetPixel((int)(perX * width), (int)(perY * height));
			if (ColorPainting.a == colorPaint.a && ColorPainting.b == colorPaint.b && ColorPainting.g==colorPaint.g && colorPaint.r == ColorPainting.r)
			{
			//	Debug.Log("isPainted");
				return;
			}
		//	Debug.Log("Painted");
			
		    
			TemPlayer.StartFloodFill();
			FloodFillBorder(CloneTexure2D, colorRegion, (int)(perX * width), (int)(perY * height), ColorPainting, new Color(0, 0, 0, 1), Input.mousePosition);
		//	CloneTexure2D.Apply();
			freetime = 0;
			TemPlayer.SetTempText(CloneTexure2D.GetPixels(), Input.mousePosition,true, (changeLayer = !changeLayer));
			loading = true;


			//((Texture2D)material.mainTexture).Apply();

		}


		


	}
	public void ApplyColor()
	{
		CloneTexure2D.Apply();
		//	GetComponent<SpriteRenderer>().sprite = Sprite.Create(CloneTexure2D, new Rect(0, 0, (int)CtrlPainting.Ins.Width, (int)CtrlPainting.Ins.Height), new Vector2(0.5f, 0.5f));
		GetComponent<SpriteRenderer>().sprite = Sprite.Create(CloneTexure2D, new Rect(0f, 0f, CloneTexure2D.width, CloneTexure2D.height), new Vector2(0.5f, 0.5f), 100F, 0, SpriteMeshType.FullRect);
	}
	
	public void SaveImg()
	{
		
     //	StartCoroutine(SaveTextureAsPNG((Texture2D)CloneTexure2D, PathSave));
		SaveTextureAsPNGNow((Texture2D)CloneTexure2D, PathSave);
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


		File.WriteAllBytes(_fullPath,_texture.EncodeToJPG(100));
		yield return new WaitForSeconds(0);

		Debug.Log("Kb was saved as: " + _fullPath);
	}

	public void SaveTextureAsPNGNow(Texture2D _texture, string _fullPath)
	{


		File.WriteAllBytes(_fullPath, _texture.EncodeToJPG(100));
	
		Debug.Log("Kb was saved as: " + _fullPath);
	}

	public void SaveToCompleted()
	{
	
		//FileUtil.DeleteFileOrDirectory(SaveFilePath);
	
		SaveTextureAsPNGNow((Texture2D)CloneTexure2D, SaveSaveCompletedPath);
		CtrlPainting.Ins.ApplyToChangeToCompled();
	}

	public void SaveToShared()
	{
		StartCoroutine(SaveTextureAsPNG((Texture2D)CloneTexure2D, SaveSharedPath));
		CtrlPainting.Ins.ApplyToChangeToShared();
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
			return Path.Combine(SaveDirectory, SourcePainting.PageConfig.UniqueId + ".png");
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
		//TopPixel = new Vector2(9999, 0); BottomPixel = new Vector2(0, 99999);
	
	
		int w = targetTexture.width;
		int h = targetTexture.height;
		Color[] colors = targetTexture.GetPixels();
		Color[] colorsBorder = aTex;
		byte[] checkedPixels = new byte[colors.Length];
		Color refCol = aBorderColor;
		Queue<Point> nodes = new Queue<Point>();
		nodes.Enqueue(new Point(aX, aY));
		float maxX = aX;
		float minX = aX;
		float maxY = aY;
		float minY = aY;
		while(nodes.Count > 0)
		{
			Point current = nodes.Dequeue();

			for (int i = current.x; i < w; i++)
			{
				
				if (checkedPixels[i + current.y * w] > 0 || colorBorder[i + current.y * w] == 1)
					break;
				maxX = Mathf.Max(maxX, i);
				minY = Mathf.Min(minY, current.y);

			//	colorTemp[i + current.y * w] = aFillColor;
				colors[i + current.y * w] = aFillColor;
				//TopPixel.x = Mathf.Min(i, TopPixel.x);
				//TopPixel.y = Mathf.Max(current.y,TopPixel.y);
				//BottomPixel.x = Mathf.Max(i, BottomPixel.x);
				//BottomPixel.y = Mathf.Min(current.y, BottomPixel.y);
				checkedPixels[i + current.y * w] = 1;
				if (current.y + 1 < h)
				{
					if (checkedPixels[i + current.y * w + w] == 0 && colorBorder[i + current.y * w + w] != 1)
						nodes.Enqueue(new Point(i, current.y + 1));
				}
				if (current.y - 1 >= 0)
				{
				
					if (checkedPixels[i + current.y * w - w] == 0 && colorBorder[i + current.y * w - w] != 1)
						nodes.Enqueue(new Point(i, current.y - 1));
				}
			}
			for (int i = current.x - 1; i >= 0; i--)
			{
				if (checkedPixels[i + current.y * w] > 0 || colorBorder[i + current.y * w] == 1)
					break;
				minX = Mathf.Min(minX, i);
				maxY = Mathf.Max(maxY, current.y);
				
			//	colorTemp[i + current.y * w] = aFillColor;
				colors[i + current.y * w] = aFillColor;
				checkedPixels[i + current.y * w] = 1;
				//TopPixel.x = Mathf.Min(i, TopPixel.x);
				//TopPixel.y = Mathf.Max(current.y, TopPixel.y);
				//BottomPixel.x = Mathf.Max(i, BottomPixel.x);
				//BottomPixel.y = Mathf.Min(current.y, BottomPixel.y);
				if (current.y + 1 < h)
				{
					if (checkedPixels[i + current.y * w + w] == 0 && colorBorder[i + current.y * w + w] != 1)
						nodes.Enqueue(new Point(i, current.y + 1));
				}
				if (current.y - 1 >= 0)
				{
					if (checkedPixels[i + current.y * w - w] == 0 && colorBorder[i + current.y * w - w] != 1)
						nodes.Enqueue(new Point(i, current.y - 1));
				}

			}

		}
	//	Debug.Log(maxX + " " + minX + "  " + maxY + "  " + minY);
		Vector2 Size = new Vector2(Mathf.Abs(maxX - maxY), Mathf.Abs(maxY - minY));
		max = Mathf.Max(Size.x, Size.y);
		targetTexture.SetPixels(colors);
	}
	public struct Point
	{
		public short x;
		public short y;
		public Point(short aX, short aY) { x = aX; y = aY; }
		public Point(int aX, int aY) : this((short)aX, (short)aY) { }
	}

	
	//void floodFillUtil(int screen[][N], int x, int y, int prevC, int newC)
	//{
	//	// Base cases 
	//	if (x < 0 || x >= M || y < 0 || y >= N)
	//		return;
	//	if (screen[x][y] != prevC)
	//		return;
	//	if (screen[x][y] == newC)
	//		return;

	//	// Replace the color at (x, y) 
	//	screen[x][y] = newC;

	//	// Recur for north, east, south and west 
	//	floodFillUtil(screen, x + 1, y, prevC, newC);
	//	floodFillUtil(screen, x - 1, y, prevC, newC);
	//	floodFillUtil(screen, x, y + 1, prevC, newC);
	//	floodFillUtil(screen, x, y - 1, prevC, newC);
	//}

	//// It mainly finds the previous color on (x, y) and 
	//// calls floodFillUtil() 
	//void floodFill(int screen[][N], int x, int y, int newC)
	//{
	//	int prevC = screen[x][y];
	//	floodFillUtil(screen, x, y, prevC, newC);
	//}

	// Driver program to test above function 

} 


