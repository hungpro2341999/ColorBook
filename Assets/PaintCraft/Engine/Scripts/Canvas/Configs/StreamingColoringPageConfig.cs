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
using PaintCraft.Canvas.Configs;
using System.IO;

namespace PaintCraft.Canvas.Configs{

    [CreateAssetMenu(menuName = "PaintCraft/StreamingColoringPageConfig")]
    public class StreamingColoringPageConfig : AdvancedPageConfig
    {
        public Sprite Icon;
        public string OutlinePngPath;
        public string RegionPngPath;

        Texture2D outlineTexture;
        Texture2D regionTexture;

        #region implemented abstract members of PageConfig
        public override Vector2 GetSize()
        {
            return new Vector2(OutlineTexture.width, OutlineTexture.height);
        }
        #endregion



        #region implemented abstract members of AdvancedPageConfig
        public override Texture2D OutlineTexture
        {
            get
            {
                if (outlineTexture == null){
                    outlineTexture = GetStreamingTexture(OutlinePngPath);
                }
                return outlineTexture;
            }
        }

        public override Texture2D RegionTexture
        {
            get
            {
                if (regionTexture == null){
                    regionTexture = GetStreamingTexture(RegionPngPath);
                }
                return regionTexture;
            }
        }
        #endregion

        Texture2D GetStreamingTexture(string texturePath){
            string path = Path.Combine(Application.streamingAssetsPath, texturePath);
            //path = "file://"+path;
            Texture2D result = new Texture2D(1,1, TextureFormat.Alpha8, false);
            #if UNITY_5_4 || UNITY_5_4_OR_NEWER
            result.LoadImage(File.ReadAllBytes(path), true);
            #else
            result.LoadImage(File.ReadAllBytes(path));
            #endif
            return result;
        }
    }
}
