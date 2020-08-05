using PaintCraft.Canvas.Configs;
using PaintCraft.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OutlineLayer : MonoBehaviour
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


		material.mainTexture = PageConfig.RegionTexture;
		mr.material = material;
		

	}
	
	private void Update()
	{
	



	}

}
