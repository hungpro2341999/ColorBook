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
using UnityEngine.Assertions;
using NodeInspector;


namespace PaintCraft.Tools.Filters.MaterialFilter{
    /// <summary>
    /// Bind start image (from page config) texture to _TileTex point material
    /// </summary>
    
    [NodeMenuItemAttribute("Material/BindTileTexToStartImage")]
    public class BindTileTexToStartImage : FilterWithNextNode {
        #region implemented abstract members of FilterWithNextNode
        public override bool FilterBody(BrushContext brushLineContext)
        {            
            if (brushLineContext.IsFirstPointInLine && brushLineContext.Canvas.PageConfig.StartImageTexture != null){                
                SetTextureParameters(brushLineContext.BasePoints.First, 
                    brushLineContext.Canvas.PageConfig.StartImageTexture);
                SetTextureParameters(brushLineContext.Points.First, 
                    brushLineContext.Canvas.PageConfig.StartImageTexture);                
            }
            return true;
        }
        #endregion

        void SetTextureParameters(LinkedListNode<Point> nodePoint, Texture2D texture)
        {
            if (nodePoint == null)
            {
                return;
            }
            nodePoint.Value.Material.SetTexture("_TileTex", texture);
            nodePoint.Value.Material.SetTextureOffset("_TileTex", Vector2.zero);
            nodePoint.Value.Material.SetTextureScale ("_TileTex", Vector2.one);
        }        
    }
}
