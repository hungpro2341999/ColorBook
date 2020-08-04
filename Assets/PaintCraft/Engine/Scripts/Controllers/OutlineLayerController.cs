/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using PaintCraft.Utils;


namespace PaintCraft.Controllers
{
	public class OutlineLayerController: MonoBehaviour {
		CanvasController canvas;
		private Material material;
		
		public void Init(CanvasController canvas){
			this.canvas = canvas;
			UpdateMeshSize();	
		}
		
		void UpdateMeshSize(){
			MeshFilter mf = GOUtil.CreateComponentIfNoExists<MeshFilter>(gameObject);
			Debug.Log("Canvas : " + canvas.Width + " " + canvas.Height);
			Mesh mesh = MeshUtil.CreatePlaneMesh(canvas.Width, canvas.Height);
			mf.mesh = mesh;
			MeshRenderer mr = GOUtil.CreateComponentIfNoExists<MeshRenderer>(gameObject);
			material =new Material(Shader.Find("Unlit/Transparent"));
			material.mainTexture = canvas.OutlineTexture;			
			mr.material = material;
		}

        public void SetNewSize(){
            MeshFilter mf = GOUtil.CreateComponentIfNoExists<MeshFilter>(gameObject);
            MeshUtil.ChangeMeshSize( mf.mesh, canvas.Width, canvas.Height);
            material.mainTexture = canvas.OutlineTexture;
        }

	}
}
