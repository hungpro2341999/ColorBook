/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using UnityEngine;
using System.Collections.Generic;
using NodeInspector;
using PaintCraft.Utils;
using Random = UnityEngine.Random;


namespace PaintCraft.Tools.Filters{
    /// <summary>
    /// Move to random offset within radius 
    /// you can also make it depends to BrushSize
    /// </summary>
	[NodeMenuItem("Randomizers/MoveToRandomOffset")]
    public class MoveToRandomOffset : FilterWithNextNode {
	    [EnumFlags]
	    public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
	    
	    [Tooltip("Radius in pixels")]
		public float Radius = 50;

	    public bool MultiplyToBrushSize;
	    
		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody(BrushContext brushLineContext)
		{						
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point =>
				{
					
					if (MultiplyToBrushSize)
					{
						point.Offset += Random.insideUnitCircle * Radius * brushLineContext.LineConfig.Scale;
					}
					else
					{
						point.Offset += Random.insideUnitCircle * Radius;
					}
					
				});
			
			return true;
		}

		#endregion


	}
}
