/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using PaintCraft.Canvas.Configs;
using UnityEngine.UI;
using PaintCraft.Canvas;
using PaintCraft.Tools;
using PatinCraft.UI;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace PaintCraft.Demo{
    public class SwitchRegionsToggleController : MonoBehaviour {
        public PageConfig NormalConfig;
        public PageConfig RegionConfig;

        public Brush NormalBrush;
        public Brush RegionBrush;

        public ChangeBrushOnClickController BrushBtnCtrl;

        void Start(){
            
            if (AppData.SelectedPageConfig != null){                
                GetComponent<Toggle>().isOn = (AppData.SelectedPageConfig == RegionConfig);
                if (AppData.SelectedPageConfig == RegionConfig){
                    BrushBtnCtrl.Brush = RegionBrush;
                } else {
                    BrushBtnCtrl.Brush = NormalBrush;
                }
            }
        }
                           
        // Update is called once per frame
        public void UpdateCurrentPage (bool useRegionConfig) {
            if (useRegionConfig){
                SetNewPageConfig(RegionConfig);
            } else {
                SetNewPageConfig(NormalConfig);
            }

        }

        void SetNewPageConfig(PageConfig pageConfig){
            if (AppData.SelectedPageConfig != pageConfig){                
                AppData.SelectedPageConfig = pageConfig;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#else
                Application.LoadLevel(Application.loadedLevelName);
#endif
            }
        }
    }       
}
