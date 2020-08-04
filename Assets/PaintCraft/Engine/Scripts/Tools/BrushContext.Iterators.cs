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
using System.Collections;
using System.Collections.Generic;


namespace PaintCraft.Tools
{
	public partial class BrushContext
	{
		
		/// <summary>
		/// Make action on each point which has pointStatus
		/// </summary>
		/// <param name="action">Action which perform on every point</param>
		/// <param name="pointStatus">only perform action which has Type</param>
		/// <param name="pointType">is it base or interpolated point</param>
		public void ForEachUncopiedToCanvasPoint(PointType pointType, Action<Point> action)
		{												
			if ((pointType & PointType.InterpolatedPoint) == PointType.InterpolatedPoint)
			{				
				IterateNodePoint(Points.Last, PointStatus.NotCopiedToCanvas, action);
			}
			if ((pointType & PointType.BasePoint) == PointType.BasePoint)
			{
				IterateNodePoint(BasePoints.Last, PointStatus.NotCopiedToCanvas, action);
			}
		}


		private static void IterateNodePoint(LinkedListNode<Point> nodePoint, PointStatus pointStatus, Action<Point> action)
		{
			while (nodePoint != null && (nodePoint.Value.Status & pointStatus) == nodePoint.Value.Status)
			{
				action(nodePoint.Value);
				nodePoint = nodePoint.Previous;
			}
		} 
	}
}
