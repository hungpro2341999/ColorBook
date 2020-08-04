/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using NodeInspector;
using UnityEngine;


namespace PaintCraft.Tools.Filters.MaterialFilter
{
	[NodeMenuItemAttribute("Conditions/IsDefaultColorOpaque")]
	public class IsDefaultColorOpaque : IFilter
	{
		[OneWay]
		public IFilter Yes;
		
		[OneWay]
		public IFilter No;


		public override void Apply(BrushContext brushLineContext)
		{
			if (brushLineContext.Canvas.DefaultBGColor.a == 1.0f)
			{
				Yes.Apply(brushLineContext);
			}
			else
			{
				No.Apply(brushLineContext);
			}
		}
	}
}
