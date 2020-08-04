using PaintCraft.Canvas.Configs;
using PaintCraft.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		material.mainTexture = PageConfig.OutlineTexture;
		mr.material = material;
		var box = gameObject.AddComponent<BoxCollider>();
	
	}
	public bool Run = false;
	private void Update()
	{
		if (!Input.GetMouseButton(0))
			return;

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




}
