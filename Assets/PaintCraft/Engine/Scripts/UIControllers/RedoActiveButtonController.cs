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
using UnityEngine.UI;
using UnityEngine.Events;

namespace PatinCraft.UI{   
    public class RedoActiveButtonController : MonoBehaviour {
        public CanvasController Canvas;
        Button button;

        void Start()
        {
            if (Canvas == null){
                Debug.LogError("you have to provide link to the Canvas for this component", gameObject);     
            }
            
            button = GetComponent<Button>();
            button.onClick.AddListener(new UnityAction(() => {
                Canvas.Redo();
            }));
        }

        void Update()
        {
        //    button.interactable = Canvas.UndoManager.HasRedo();
        }
    }
}
