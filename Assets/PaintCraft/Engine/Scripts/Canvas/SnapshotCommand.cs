/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using ColoringBook.Controllers;


namespace PaintCraft.Canvas{
	public class SnapshotCommand : ICanvasCommand {
		UndoManager undoManager;
		SnapshotData stateBefore;
		SnapshotData stateAfter;


		public SnapshotCommand(UndoManager undoManager){
			this.undoManager = undoManager;
		}

		#region ICanvasCommand implementation

		public void BeforeCommand ()
		{
			stateBefore = undoManager.SnapshotPool.GetCurrentSnapshotData();
			if (stateBefore == null){
				//very first drawing here
				stateBefore = undoManager.SnapshotPool.MakeSnapshot();
			}
		}

		public void AfterCommand ()
		{
			if (!undoManager.SnapshotPool.HasFreeSnapshotSlot()){
				undoManager.RemoveFirstSnapshotFromHistory();
			}
			stateAfter = undoManager.SnapshotPool.MakeSnapshot();
		}

		public void Undo ()
		{
			undoManager.SnapshotPool.Undo(stateBefore);
		}

		public void Redo ()
		{
			undoManager.SnapshotPool.Redo(stateAfter);
		}
		#endregion
	}
}
