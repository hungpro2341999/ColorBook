/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace PaintCraft.Demo.ColoringBook{    
    
    public class ExitToGalleryByExitButtonController : MonoBehaviour {
        
        void Update () {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
                SceneManager.LoadScene("PageSelect");
#else
                Application.LoadLevel("PageSelect");
#endif
            }               
        }
    }    
}
