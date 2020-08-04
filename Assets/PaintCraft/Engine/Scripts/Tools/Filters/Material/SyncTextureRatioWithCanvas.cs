/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.MaterialFilter{
    public enum TextureName{
        Main,
        Tile
    }

    
    /// <summary>
    /// Set point texture ratio property the same as pageconfig size
    /// </summary>
    [NodeMenuItemAttribute("Material/SyncTextureRatioWithCanvas")]
    public class SyncTextureRatioWithCanvas : FilterWithNextNode {
        [EnumFlags]
        public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
        public TextureName TextureNameInShader = TextureName.Main;
        public float HorizontalScale = 1.0f;
        public float VerticalScale = 1.0f;
        #region implemented abstract members of FilterWithNextNode
        public override bool FilterBody(BrushContext brushLineContext)
        {
            string textureName = TextureNameInShader == TextureName.Main ? "_MainTex" : "_TileTex";

            Texture materialTexture;
            float ratioX;
            float ratioY;
            brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
                point =>
                {
                    materialTexture = point.Material.GetTexture(textureName);
                    Assert.IsNotNull(materialTexture, textureName + " texture must be set");
                    ratioX = brushLineContext.Canvas.Width / (float)materialTexture.width / HorizontalScale;
                    ratioY = brushLineContext.Canvas.Height / (float)materialTexture.height / VerticalScale;
                    point.Material.SetTextureScale(textureName, new Vector2(ratioX, ratioY));                                        
                });            
            return true;
        }
        #endregion
        
    }
}
