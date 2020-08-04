/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using PaintCraft.Tools;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace PatinCraft.UI{
    public class ChangeColorOnClickController : MonoBehaviour, IPointerClickHandler {
        public LineConfig LineConfig;
        public Color Color;
    	
    	void Start () {
            if (LineConfig == null){
                Debug.LogError("LineConfig must be provided", gameObject);
            }
    	}
    	

        #region IPointerClickHandler implementation
        public void OnPointerClick(PointerEventData eventData)
        {
            LineConfig.Color.Color = Color;
        }
        #endregion

        [ContextMenu("Copy Color from Image")]
        public void CopyColorImageComponent(){
            Image image = GetComponent<Image>();
            if (image != null){                
                this.Color = image.color;
            }
        }
    }
}
