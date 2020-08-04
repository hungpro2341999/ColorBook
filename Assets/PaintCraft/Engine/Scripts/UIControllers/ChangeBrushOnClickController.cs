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
using PaintCraft.Tools;
using UnityEngine.EventSystems;

namespace PatinCraft.UI{
    
    public class ChangeBrushOnClickController : MonoBehaviour, IPointerClickHandler {
        public LineConfig LineConfig;
        public Brush Brush;

        void Awake(){            
            if (LineConfig == null){
                Debug.LogError("LineConfig must be provided", gameObject);
            }

            if (Brush == null){
                Debug.LogError("Brush could not be null here", gameObject);
            }
        
            Toggle t = GetComponent<Toggle>();
            if (t!= null){
                string usedBrushName = PlayerPrefs.GetString(LineConfig.name);
                if (string.IsNullOrEmpty(usedBrushName)){
                    t.isOn = Brush == LineConfig.Brush;
                } else {                    
                    t.isOn = Brush.name == usedBrushName;
                    if (t.isOn){                        
                        LineConfig.Brush = Brush;
                    }
                }
            }

        }            
            
        #region IPointerClickHandler implementation

        public void OnPointerClick(PointerEventData eventData)
        {
            LineConfig.Brush = Brush;
            PlayerPrefs.SetString(LineConfig.name, Brush.name);
            PlayerPrefs.Save();
        }

        #endregion
               
    }        
}
