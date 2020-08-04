/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using NodeInspector;
using UnityEngine;

namespace PaintCraft.Tools.Filter.ChangePoint{
	/// <summary>
	/// Remove duplicate last input positions.
	/// if we received the same position from Input. just remove last so we will have just one point with that position
	/// </summary>
    [NodeMenuItem("ChangePoint/RemoveDuplicateLastInputPositions")]
    public class RemoveDuplicateLastInputPositions : FilterWithNextNode {
		#region implemented abstract members of FilterWithNextNode

		public override bool FilterBody (BrushContext brushLineContext)
		{
            if (brushLineContext.BasePoints.Count > 1 
                && brushLineContext.BasePoints.Last.Value.Position == brushLineContext.BasePoints.Last.Previous.Value.Position){
                brushLineContext.RemoveLastBaseNodePointAndReturnToPull();	            
	            return false;
            }

			return true;
		}

		#endregion



	}
}
