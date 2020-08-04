/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;


namespace PaintCraft.Demo.ColoringBook{ 
    public class MoveScrollContentButtonController : MonoBehaviour
    {
        public ScrollRect ScrollRect;
        public Vector2 VelocityPerClick;
        public Vector2 LockOnPosition;
        Button button;
    	
    	void Start ()
    	{
    	    button = GetComponent<Button>();
    	    button.onClick.AddListener(OnButtonClicked);
    	}

        void OnButtonClicked()
        {
           
            ScrollRect.normalizedPosition = new Vector2(VelocityPerClick.x != 0.0f ?  Mathf.Clamp(ScrollRect.normalizedPosition.x, 0.001f, 0.999f) : 0.0f,
               VelocityPerClick.y != 0.0f ? Mathf.Clamp(ScrollRect.normalizedPosition.y, 0.001f, 0.999f) : 0.0f);   
             ScrollRect.velocity+= VelocityPerClick;
        }

    	void Update ()
    	{
    	    button.interactable = ScrollRect.normalizedPosition != LockOnPosition;
    	}
    }
}
