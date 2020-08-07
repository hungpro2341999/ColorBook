/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using PaintCraft.Utils;
using UnityEngine;

namespace PaintCraft.Canvas.Configs
{
    [CreateAssetMenu(menuName = "PaintCraft/ColoringPageConfig")]
    public class ColoringPageConfig : AdvancedPageConfig
    {

    

         
        public Texture2D outlineTexture;

        public override Texture2D OutlineTexture
        {
            get
            {
               
                return outlineTexture;
            }
        }



      
        public Texture2D regionTexture;
        public override Texture2D RegionTexture
        {
            get
            {
              
                return regionTexture;
            }
        }

        public Sprite Icon()
        {
           return null;
        }

     
        public override Vector2 GetSize()
        {
            
          
                return new Vector2(OutlineTexture.width, OutlineTexture.height);
            
        }
    }
}
