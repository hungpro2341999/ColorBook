/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Collections;
using System;


namespace PaintCraft.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CanvasCameraController : MonoBehaviour {
		public Camera Camera {get; private set;}
        [NonSerialized]
        public bool Initialized = false;
        CanvasController canvasCtrl;

        CommandBuffer commandBuffer;
        public CommandBuffer CommandBuffer{
            get{
                return commandBuffer;
            }
        }

        public void Init(CanvasController canvas){
            Camera = GetComponent<Camera>();
            if (Camera == null){
                Camera = gameObject.AddComponent<Camera>();
            }

            commandBuffer = new CommandBuffer();
            commandBuffer.name = "paintcraft canvas";
            Camera.AddCommandBuffer(CameraEvent.AfterEverything, commandBuffer);     


			Camera.orthographic = true;
		//	Camera.clearFlags = CameraClearFlags.Color;
			Camera.backgroundColor = Color.black;
			Camera.orthographicSize =  (float)canvas.Height / 2.0f;
			Camera.aspect = (float)canvas.Width / (float)canvas.Height;

			
			Camera.clearFlags= CameraClearFlags.Nothing;
            canvasCtrl = canvas;
            Camera.targetTexture = canvasCtrl.BackLayerController.RenderTexture;
            Initialized = true;
		}
              
        public void SetNewSize(){
            Camera.targetTexture = canvasCtrl.BackLayerController.RenderTexture;
            Camera.targetTexture = canvasCtrl.BackLayerController.RenderTexture;
            Camera.orthographicSize =  (float)canvasCtrl.Height / 2.0f;
            Camera.aspect = (float)canvasCtrl.Width / (float)canvasCtrl.Height;
        }


        bool clearCanvasBGTexture = false;
        System.Action onClearDone;
        public void ClearRenderTexture(System.Action onClearDone){
            clearCanvasBGTexture = true;
            this.onClearDone = onClearDone;
        }



        void OnPostRender(){
            if (clearCanvasBGTexture){
                clearCanvasBGTexture = false;
                ClearCanvasBGTexture() ;   
            }

        }


        void OnEnable(){
            StartCoroutine("ClearCommandBuffer");
        }

        void OnDisable(){
            StopCoroutine("ClearCommandBuffer");
        }

        IEnumerator ClearCommandBuffer(){
            while(true){
                yield return new WaitForEndOfFrame();
                commandBuffer.Clear();
            }
        }

        void ClearCanvasBGTexture(){            
            AnalyticsWrapper.CustomEvent("ClearCanvas", new Dictionary<string, object>());
            RenderTexture currentRT  = RenderTexture.active;
            RenderTexture.active = Camera.targetTexture;
            if (canvasCtrl.PageConfig.StartImageTexture == null){                
                Camera.targetTexture.DiscardContents();
                GL.Clear(false, true, canvasCtrl.DefaultBGColor);
            } else {
                Graphics.Blit(canvasCtrl.PageConfig.StartImageTexture, Camera.targetTexture);
            }
            RenderTexture.active = currentRT;
            if (onClearDone != null){
                onClearDone();
                onClearDone = null;
            }
        }
    }
}
