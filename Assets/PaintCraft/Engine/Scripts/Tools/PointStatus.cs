/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/


namespace PaintCraft.Tools{	
	[System.Flags]
	public enum PointStatus : int {
		NotSet = 0,
		PositionSet = 1<<1,
		Temporary = 1 << 2,
		ReadyToApply = 1 << 3, //could be copied to canvas
		CopiedToCanvas  = 1 << 4,
		NotCopiedToCanvas = PointStatus.PositionSet | PointStatus.Temporary | PointStatus.ReadyToApply
	}
}
