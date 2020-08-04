/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using NodeInspector;
using UnityEngine.Serialization;


namespace PaintCraft.Tools.Filters.ChangePoint{
	/// <summary>
	/// Set last base point Ready to apply
	/// </summary>
    [NodeMenuItem("ChangePoint/SetLastPointReadyToApply")]	
    public class SetLastPointReadyToApply : FilterWithNextNode {
		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody (BrushContext brushLineContext)
		{
			brushLineContext.BasePoints.Last.Value.Status = PointStatus.ReadyToApply;
			return true;
		}
		#endregion
		

	}
}
