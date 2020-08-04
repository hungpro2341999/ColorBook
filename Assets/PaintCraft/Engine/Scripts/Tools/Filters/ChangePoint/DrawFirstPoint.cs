/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using PaintCraft.Tools;
using NodeInspector;


namespace PaintCraft.Tools.Filters.ChangePoint{
	
	/// <summary>
	/// Set first base point status ready to apply
	/// </summary>
	[NodeMenuItem("ChangePoint/DrawFirstPoint")]
    public class DrawFirstPoint : FilterWithNextNode {        
		#region implemented abstract members of FilterWithNextNode
        public override bool FilterBody (BrushContext brushLineContext)
		{
			if (brushLineContext.BasePoints.First.Value.IsFirstBasePointFromInput){
				if (brushLineContext.BasePoints.First.Value.Status != PointStatus.CopiedToCanvas){
					if (brushLineContext.BasePoints.Count > 1){
						brushLineContext.BasePoints.First.Value.Status = PointStatus.ReadyToApply;
					} else {
						brushLineContext.BasePoints.First.Value.Status = PointStatus.Temporary;
					}
				}
			}
            return true;
		}
		#endregion
	}
}
