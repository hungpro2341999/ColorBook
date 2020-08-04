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
	/// Set random scale to the point
	/// </summary>
	[NodeMenuItem("Randomizers/RandomScale")]
    public class RandomScale : FilterWithNextNode {
		[EnumFlags]
		public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
		
		public float ScaleMax = 1.0f;
		public float ScaleMin = 0.1f;

		#region implemented abstract members of FilterWithNextNode
		
		public override bool FilterBody (BrushContext brushLineContext)
		{		
			float baseScale = brushLineContext.LineConfig.Scale;
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point => point.Scale = Random.Range(ScaleMin, ScaleMax) * baseScale);						
			return true;
		}
		
		#endregion

	}
}
