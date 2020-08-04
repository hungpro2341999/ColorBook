/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filter.ChangePoint{
	/// <summary>
	/// Iterate starting from FIRST POINT
	/// Remove all aplied points and left just one which has status CopiedToCanvas
	/// </summary>
    [NodeMenuItem("ChangePoint/RemoveAppliedPointsExceptLast")]
    public class RemoveAppliedPointsExceptLast : FilterWithNextNode {
	    [EnumFlags]
	    public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
	    #region implemented abstract members of FilterWithNextNode

		public override bool FilterBody (BrushContext brushLineContext)
		{
			if ((PointType & PointType.InterpolatedPoint) == PointType.InterpolatedPoint)
			{				
				while (brushLineContext.Points.Count > 1 
				       && brushLineContext.Points.First.Value.Status == PointStatus.CopiedToCanvas 
				       && brushLineContext.Points.First.Next != null
				       && brushLineContext.Points.First.Next.Value.Status == PointStatus.CopiedToCanvas){
					brushLineContext.RemoveFirstNodePointAndReturnToPull();
				}	
			}
			if ((PointType & PointType.BasePoint) == PointType.BasePoint)
			{
				while (brushLineContext.BasePoints.Count > 1 
				       && brushLineContext.BasePoints.First.Value.Status == PointStatus.CopiedToCanvas 
				       && brushLineContext.BasePoints.First.Next != null
				       && brushLineContext.BasePoints.First.Next.Value.Status == PointStatus.CopiedToCanvas){
					brushLineContext.RemoveFirstBaseNodePointAndReturnToPull();
				}
			}			
			return true;
		}

		#endregion



	}
}
