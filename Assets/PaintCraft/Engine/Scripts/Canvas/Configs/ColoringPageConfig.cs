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

        [TexturePath] public string outlinePath;
        [TexturePath] public string RegionPath;
        [TexturePath] public string IconPath;

        [NonSerialized]
        Texture2D outlineTexture;

        public override Texture2D OutlineTexture
        {
            get
            {
                if (outlineTexture == null)
                {
                    outlineTexture = Resources.Load<Texture2D>(OutlinePath);
                }
                return outlineTexture;
            }
        }



        [NonSerialized]
        Texture2D regionTexture;
        public override Texture2D RegionTexture
        {
            get
            {
                if (regionTexture == null)
                {
                    regionTexture = Resources.Load<Texture2D>(RegionPath);
                }
                return regionTexture;
            }
        }

        public Sprite Icon
        {
            get { return Resources.Load<Sprite>(IconPath); }
        }

        public string OutlinePath
        {
            get
            {
                return outlinePath;
            }

            set
            {
                outlinePath = value;
            }
        }

        public override Vector2 GetSize()
        {
            
            if (OutlineTexture == null)
            {
                if (StartImageTexture == null) {                    
                    if (RegionTexture == null){                        
                        Debug.Log("one of Outline, StartImage or Region picture must be set", this);
                        return Vector2.one;
                    } else {
                        return new Vector2(RegionTexture.width, RegionTexture.height);
                    }
                } else {
                    return new Vector2(StartImageTexture.width, StartImageTexture.height);
                }               
            }
            else
            {
                return new Vector2(OutlineTexture.width, OutlineTexture.height);
            }
        }
    }
}
