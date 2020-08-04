/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using NodeInspector;


namespace PaintCraft.Tools.Filter.ChangePoint{
	/// <summary>
	/// Check distance to previous BasePoint less than Spacing
	/// </summary>
	[NodeMenuItem("ChangePoint/IgnorePositionsCloseThanSpacing")]
    public class IgnorePositionsCloseThanSpacing : FilterWithNextNode {
		public SpacingProperty Spacing; 
		#region implemented abstract members of FilterWithNextNode

        public override bool FilterBody (BrushContext brushLineContext)
		{			
            bool latestPointRemoved = brushLineContext.RemoveLastBasePointIfDistanceToPreviousBasePointLessThan(Spacing.GetSpacingValue(brushLineContext));
            return !latestPointRemoved;
		}

		#endregion


	}
}
