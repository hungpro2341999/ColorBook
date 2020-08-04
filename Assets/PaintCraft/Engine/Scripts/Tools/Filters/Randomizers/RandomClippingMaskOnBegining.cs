/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using NodeInspector;

namespace PaintCraft.Tools.Filters.Randomizers{
	/// <summary>
	/// Change brush context clipping mask offset
	/// </summary>
    [NodeMenuItem("Randomizers/RandomClippingMaskOffsetOnBegining")]
    public class RandomClippingMaskOnBegining : FilterWithNextNode {
		#region implemented abstract members of FilterWithNextNode

		Vector2 baseVector;
		public override bool FilterBody (BrushContext brushLineContext)
		{
            if (brushLineContext.IsFirstPointInLine
			    && brushLineContext.BasePoints.First.Value.BasePointId == 0){
                baseVector = new Vector2(Random.value * 100.0f, Random.value * 100.0f);                         
            } else {
                baseVector.y += Time.deltaTime * 0.01f;
            }
            brushLineContext.ClippingMaskOffset = baseVector;			
			return true;
		}

		#endregion
			
	}
}
