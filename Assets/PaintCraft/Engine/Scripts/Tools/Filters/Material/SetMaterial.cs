/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections.Generic;
using UnityEngine;
using NodeInspector;
using PaintCraft.Canvas.Configs;
using PaintCraft.Utils;


namespace PaintCraft.Tools.Filters.MaterialFilter{

    /// <summary>
    /// Set point material from filter Material property
    /// </summary>    
    [NodeMenuItemAttribute("Material/SetMaterial")]
    public class SetMaterial : FilterWithNextNode {
        [EnumFlags]
        public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
        
        public Material Material;

        Material _material;

        #region implemented abstract members of FilterWithNextNode
        public override bool FilterBody(BrushContext brushLineContext)
        {
            if (brushLineContext.IsFirstPointInLine && _material == null){
                _material = new Material(Material);
            }

            if (_material.HasProperty("_RegionTex") && brushLineContext.IsFirstPointInLine
                && typeof(AdvancedPageConfig).IsAssignableFrom(brushLineContext.Canvas.PageConfig.GetType()) 
                && ((ColoringPageConfig)brushLineContext.Canvas.PageConfig).RegionTexture != null ){
                _material.SetTexture("_RegionTex", ((ColoringPageConfig)brushLineContext.Canvas.PageConfig).RegionTexture);

                _material.SetFloat("_OriginX", brushLineContext.FirstPointUVPosition.x);
                _material.SetFloat("_OriginY", brushLineContext.FirstPointUVPosition.y);
            }
                           
            brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
                point => point.Material = _material);
                        
            return true;
        }
        #endregion
        
    }
}
