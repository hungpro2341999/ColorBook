/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintCraft.Tools;
using UnityEngine.Assertions;


namespace PaintCraft.Controllers{
    public class DrawingGameObjectController : MonoBehaviour {
        public int LineId; // must be unique acros all ines included touch ids
        public LineConfig LineConfig;
        public ScreenCameraController ScreenCameraController;

        bool _previousStatus = false;

        public bool IsEnabled = true;



        void Start(){
            Assert.IsNotNull(LineConfig);
            Assert.IsNotNull(ScreenCameraController);
        }

        void Update(){
            if (Time.frameCount < 20)
            {
                return;
            }
            if (IsEnabled){
                if (!_previousStatus){
                    ScreenCameraController.BeginLine(LineConfig, LineId, transform.position);
                } else {
                    ScreenCameraController.ContinueLine(LineId, transform.position);
                }
            } else{
                if (_previousStatus){
                    ScreenCameraController.EndLine(LineId, transform.position);
                } else {
                    //do nothing
                }
            }
                
            _previousStatus = IsEnabled;
        }
                   

        public void SetStatus(bool status){
            IsEnabled = status;
        }
    }
}
