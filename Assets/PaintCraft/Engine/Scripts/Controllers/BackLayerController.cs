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
	public class BackLayerController : MonoBehaviour {
        
        public RenderTexture RenderTexture { get ; private set;}
		CanvasController canvas;
    

		public void Init(CanvasController canvas){
			this.canvas = canvas;
			UpdateMeshSize();	
		}

        void UpdateMeshSize(){    
			MeshFilter mf = GOUtil.CreateComponentIfNoExists<MeshFilter>(gameObject);
			Mesh mesh = MeshUtil.CreatePlaneMesh(canvas.Width, canvas.Height);
			mf.mesh = mesh;
			MeshRenderer mr = GOUtil.CreateComponentIfNoExists<MeshRenderer>(gameObject);

	        string shaderName =  "Unlit/Transparent";
	        mr.material = new Material(Shader.Find(shaderName));
            RenderTexture = TextureUtil.SetupRenderTextureOnMaterial(mr.material, canvas.RenderTextureSize.x, canvas.RenderTextureSize.y);
		}

        public void SetNewSize(){                
            canvas.CanvasCameraController.Camera.targetTexture = null;
            RenderTexture = TextureUtil.UpdateRenderTextureSize(RenderTexture, canvas.RenderTextureSize.x, canvas.RenderTextureSize.y);
            GetComponent<MeshRenderer>().material.mainTexture = RenderTexture;
            canvas.CanvasCameraController.Camera.targetTexture = RenderTexture;
            MeshUtil.ChangeMeshSize(GetComponent<MeshFilter>().mesh, canvas.RenderTextureSize.x, canvas.RenderTextureSize.y);
        }
	}
}
