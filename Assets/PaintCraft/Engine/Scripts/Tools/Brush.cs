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
using NodeInspector;

namespace PaintCraft.Tools{
	[CreateAssetMenu(menuName="PaintCraft/Brush")]
	public class Brush : ScriptableObject
	{	    
        [Graph("StartFilter")]
	    public List<IFilter> Filters;
        public IFilter StartFilter;
		public Vector2 MinSize = Vector2.one;		
		public Vector2 BaseSize = new Vector2(100.0f, 100.0f);
	}
}


