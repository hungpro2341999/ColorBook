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
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.Randomizers
{
    
    /// <summary>
    /// Set random color (R,G,B) to the Point
    /// </summary>
    [NodeMenuItem("Randomizers/RandomColor")]
    public class RandomColor : FilterWithNextNode
    {
        [EnumFlags]
        public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
        
        #region implemented abstract members of FilterWithNextNode
        public override bool FilterBody(BrushContext brushLineContext)
        {            
            brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
                point => point.PointColor.Color = new Color(Random.value,Random.value,Random.value, 1.0f));
            return true;
        }

        #endregion

    }
}
