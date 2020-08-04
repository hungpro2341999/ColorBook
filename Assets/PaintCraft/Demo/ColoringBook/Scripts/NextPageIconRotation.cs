/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

namespace PaintCraft.Demo.ColoringBook{ 
    public class NextPageIconRotation : MonoBehaviour {
    	public RectTransform ContentRect;

    	public float MiddleYPosition = 1030.0f;

    	RectTransform selfRectTransform;
    	void Start(){
    		selfRectTransform = GetComponent<RectTransform>();
    	}


    	// Update is called once per frame
    	void Update () {
    		float rotation = (ContentRect.anchoredPosition.y / MiddleYPosition) * 180.0f;
    		selfRectTransform.localRotation = Quaternion.Euler(0,0,rotation);
    	}
    }
}
