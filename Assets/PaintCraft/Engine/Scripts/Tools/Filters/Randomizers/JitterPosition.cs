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

namespace PaintCraft.Tools.Filters.Randomizers{
    /// <summary>
    /// Change point position to random value within specific radius
    /// </summary>
	[NodeMenuItem("Randomizers/JitterPosition")]
    public class JitterPosition : FilterWithNextNode {
	    [EnumFlags]
	    public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
	    
	    public float Offset = 5.0f;
		#region implemented abstract members of FilterWithNextNode
		
        public override bool FilterBody(BrushContext brushLineContext)
		{			
			float t, u, r;
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point =>
				{
					t = 2.0f * Mathf.PI * Random.value;
					u = Random.value + Random.value;
					r = Offset * (u > 1 ? 2 - u : u);
			
					point.Offset.x = r * Mathf.Cos(t);
					point.Offset.y = r * Mathf.Sin(t);					
				});
			
			return true;
		}
		
		#endregion
		
	}
}
