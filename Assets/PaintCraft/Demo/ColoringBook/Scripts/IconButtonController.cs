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
using System.IO;
using PaintCraft.Canvas;
using PaintCraft.Canvas.Configs;
using PaintCraft;
using UnityEngine.UI;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace PaintCraft.Demo.ColoringBook{ 

    public class IconButtonController : MonoBehaviour
    {
        public PageConfig Page;
    	
    	void Start ()
    	{
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
            

            if (File.Exists(Page.IconSavePath) && Page.name!=null)
    	    {
                Texture2D tex = new Texture2D(440,330, TextureFormat.RGB24, false);
    	        if (tex.LoadImage(File.ReadAllBytes(Page.IconSavePath)))
    	        {
                    tex.Apply(false, true);
                    button.targetGraphic.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, 440, 330), new Vector2(0.5f, 0.5f));
                }            
    	    }

            
    	    if (Page is ColoringPageConfig)
    	    {
                transform.GetChild(0).GetComponent<Image>().sprite = (Page as ColoringPageConfig).Icon;                
    	    }            
        }

        void OnButtonClick()
        {
            AppData.SelectedPageConfig = Page;
            AnalyticsWrapper.CustomEvent("SelectPicture", new Dictionary<string, object>
            {
                { "PictureName", Page.name}        
            });
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            SceneManager.LoadScene("ColoringBook");
#else
	        Application.LoadLevel("ColoringBook");
#endif
        }        
    }
}
