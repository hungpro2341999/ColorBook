/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using PaintCraft.Controllers;
using UnityEngine.Assertions;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class RectToCanvasPosition : MonoBehaviour {
    public ScreenCameraController ScreenCameraController;
    RectTransform _rt;
	private CanvasScaler _cs;
	
    void Awake(){
        _rt =GetComponent<RectTransform>();
        _rt.hasChanged = true;
        Assert.IsNotNull(_rt);
	    _cs = GetComponentInParent<CanvasScaler>();
    }
	
	void Update () {
	    if (_rt.hasChanged)
	    {   
			_rt.hasChanged = false;
			Vector3[] corners = GetScreenRect(_rt);
		    
			ScreenCameraController.CameraSize.ViewPortOffset.left = (int)corners[0].x;
			ScreenCameraController.CameraSize.ViewPortOffset.bottom = (int)corners[0].y;
			ScreenCameraController.CameraSize.ViewPortOffset.right = Screen.width - (int)corners[2].x;
			ScreenCameraController.CameraSize.ViewPortOffset.top = Screen.height - (int)corners[2].y;
			ScreenCameraController.CameraSize.Init(ScreenCameraController.Camera, ScreenCameraController.Canvas);
		    ScreenCameraController.CameraSize.ResetSize();
	    }
	}

    public Vector3[] GetScreenRect(RectTransform rectTransform)
    {   
        
        Vector3[] corners  = new Vector3[4];
	    rectTransform.GetWorldCorners(corners);
	    if (_cs != null)
	    {
		    for (int j = 0; j < 4; j++)
		    {
			    corners[j] = corners[j] * _cs.scaleFactor;
		    }
	    }
        return corners;
    }    
}
