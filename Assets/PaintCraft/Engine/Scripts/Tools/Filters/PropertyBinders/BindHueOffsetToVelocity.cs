/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections.Generic;
using PaintCraft.Utils;
using UnityEngine;
using NodeInspector;

namespace PaintCraft.Tools.Filters.PropertyBinders{
	/// <summary>
	/// Take velocity (how fast you move point) and set point Hue
	/// </summary>
	[NodeMenuItem("PropertyBinders/BindHueOffsetToVelocity")]
    public class BindHueOffsetToVelocity : FilterWithNextNode {
		[EnumFlags]
		public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
		
		public VelocityHueProp Min = new VelocityHueProp(){ Hue = 0.0f, Velocity = 500.0f};
		public VelocityHueProp Max = new VelocityHueProp(){ Hue = 0.1f, Velocity = 1000.0f};


		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody (BrushContext brushLineContext)
		{

			float newHue;
			float pointVelocity;
			LinkedListNode<Point> node = brushLineContext.Points.Last;
			float hueDiff = Min.Hue - Max.Hue;
			float velocityDiff = Max.Velocity - Min.Velocity;			
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point =>
				{
					pointVelocity = point.Velocity;
					if (pointVelocity <= Min.Velocity){
						newHue = Min.Hue;
					} else if (pointVelocity >= Max.Velocity){
						newHue = Max.Hue;
					} else {
						newHue = Min.Hue -  hueDiff * (pointVelocity - Min.Velocity)/ velocityDiff;
					}

					point.PointColor.H  = MathUtil.LoopValue( newHue + point.PointColor.H, 0.0f, 1.0f);					
				});
			
			return true;
		}
		#endregion
			


		[System.Serializable]
		public class VelocityHueProp{
			public float Hue;
			[Tooltip("Velocity (Pixels per Seconds)")]
			public float Velocity;
		}
	}
}
