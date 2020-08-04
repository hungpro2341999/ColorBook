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


namespace PaintCraft.Tools.CustomFilters.Logic{
    [NodeMenuItemAttribute("Custom/SetDifferentMaterials")]
    public class SetDifferentMaterials : FilterWithNextNode {
        public Material FirstPointMaterial ;
        public Material RemainingPointsMaterial;

        #region implemented abstract members of FilterWithNextNode
        public override bool FilterBody(BrushContext brushLineContext)
        {            
            LinkedListNode<Point> node = brushLineContext.Points.Last;
            bool firstNotSet = true;
            while (node != null) {
                if (firstNotSet && !node.Value.IsBasePoint){                    
                    node.Value.Material = FirstPointMaterial;
                    firstNotSet = false;
                } else {                    
                    node.Value.Material = RemainingPointsMaterial;
                }
                node = node.Previous;
            }
            return true;
        }
        #endregion
        
       
    }
}
