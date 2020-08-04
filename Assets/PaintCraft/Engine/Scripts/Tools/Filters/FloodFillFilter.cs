/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections.Generic;
using PaintCraft.Tools;
using NodeInspector;


namespace PaintCraft.Tools.Filters{
	/// <summary>
	/// This single filter handle everything related to floodfill
	/// </summary>
	[NodeMenuItem("Predefined Regions/FloodFillRegion")]
    public class FloodFillFilter : FilterWithNextNode {
		#region implemented abstract members of FilterWithNextNode
        public override bool FilterBody (BrushContext brushLineContext)
		{
			while(brushLineContext.BasePoints.Count > 0){
				brushLineContext.RemoveLastBaseNodePointAndReturnToPull();			
			}

            if (brushLineContext.IsLastPointInLine) {
				LinkedListNode<Point> node = BrushContext.GetPointNode();
				node.Value.Position = brushLineContext.Canvas.transform.position;
				node.Value.Status = PointStatus.ReadyToApply;
				node.Value.Size   = brushLineContext.Canvas.Size;
				node.Value.Scale  = 1.0f;
			    node.Value.PointColor = brushLineContext.LineConfig.Color;
			    brushLineContext.Points.AddLast(node);			   
				return true;
			} else {
				return false;
			}
		}
		#endregion



	}
}
