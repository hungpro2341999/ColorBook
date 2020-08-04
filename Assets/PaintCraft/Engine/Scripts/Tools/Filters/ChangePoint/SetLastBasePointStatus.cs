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
using UnityEngine;
using NodeInspector;
using System;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.ChangePoint{
    [System.Flags]
    public enum PointState {
        FirstPoint      = 1<<0,
        MiddlePoints    = 1<<1,
        LastPoint       = 1<<2
    }

    /// <summary>
    /// Set interpolated point status
    /// </summary>
    [NodeMenuItemAttribute("ChangePoint/SetLastBasePointStatus")]
    public class SetLastBasePointStatus : FilterWithNextNode {
        public PointStatus PointStatus = PointStatus.ReadyToApply;

        [EnumFlags]
        public PointState PointState = PointState.FirstPoint | PointState.MiddlePoints | PointState.LastPoint;


        #region implemented abstract members of FilterWithNextNode
        public override bool FilterBody (BrushContext brushLineContext)
        {
            bool handleFirst = (brushLineContext.IsFirstPointInLine && ((PointState & PointState.FirstPoint) == PointState.FirstPoint));
            bool handleLast = (brushLineContext.IsLastPointInLine && ((PointState & PointState.LastPoint) == PointState.LastPoint));
            bool handleMiddle = (brushLineContext.IsFirstPointInLine == false && brushLineContext.IsLastPointInLine == false 
                && ((PointState & PointState.MiddlePoints) == PointState.MiddlePoints));
            
            if (handleFirst || handleLast || handleMiddle){
                LinkedListNode<Point> node = brushLineContext.BasePoints.Last;
                if (node != null){
                    node.Value.Status = PointStatus; 
                }               
            }
            return true;
        }
        #endregion
    }
}
