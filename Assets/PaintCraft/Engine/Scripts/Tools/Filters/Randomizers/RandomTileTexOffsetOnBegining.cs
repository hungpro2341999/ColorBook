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
using NodeInspector;
using PaintCraft.Utils;
using UnityEngine.Serialization;


namespace PaintCraft.Tools.Filters.Randomizers{
    /// <summary>
    /// Set random offset to point _TileTex texture
    /// </summary>
    [NodeMenuItemAttribute("Material/RandomTileTexOffsetOnBegining")]
    public class RandomTileTexOffsetOnBegining : FilterWithNextNode {
        [EnumFlags]
        public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
        
        #region implemented abstract members of FilterWithNextNode
        public override bool FilterBody(BrushContext brushLineContext)
        {
            if (brushLineContext.IsFirstPointInLine
                && brushLineContext.BasePoints.First.Value.IsBasePoint
                && brushLineContext.BasePoints.First.Value.BasePointId == 0){
                Vector2 offset = new Vector2(Random.value * 100.0f, Random.value * 100.0f);
                brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
                    point => point.Material.SetTextureOffset("_TileTex", offset));                
            } 
            
            
            return true;
        }
        #endregion
    }
}
