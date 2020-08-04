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
using PaintCraft.Utils;
using UnityEngine.Rendering;
using PaintCraft.Controllers;



namespace PaintCraft.Tools.Filters{
    /// <summary>
    /// Render swatch on screen or on canvas using camera COMMAND BUFFER
    ///  Be carefull this filter render only interpolated point NOT THE BASE POINT 
    /// </summary> 
    [NodeMenuItemAttribute ("Renderer/RenderSwatchWithPointMaterial(commandbuffer)")]
    public class RenderSwatchCB : FilterWithNextNode {        
        int lastUsedFrame = -1;

        #region implemented abstract members of FilterWithNextNode
        public override bool FilterBody (BrushContext brushLineContext)
        {
            CommandBuffer tempCB = null;
            if (brushLineContext.SourceInputHandler is ScreenCameraController){
                tempCB = (brushLineContext.SourceInputHandler as ScreenCameraController).CommandBuffer;
            }
           
            Vector2 firstPointUV = brushLineContext.FirstPointUVPosition;

            LinkedListNode<Point> point = null;
           
            point = brushLineContext.Points.Last;
            Mesh mesh;
            Matrix4x4 matrix;
            int i = 0;
            int j = 0;
            MeshPool meshPool = MeshPool.Instance;
            while (point != null ){                
                if (!point.Value.IsBasePoint){
                    Vector3 pointPosition = point.Value.Position + point.Value.Offset;
                    pointPosition.z = brushLineContext.Canvas.transform.position.z + brushLineContext.Canvas.BrushOffset;
                    matrix = Matrix4x4.TRS(pointPosition, Quaternion.Euler(0,0, point.Value.Rotation), Vector3.one);
                    mesh = meshPool.GetMesh(brushLineContext, point.Value, firstPointUV);
                    if (point.Value.Status == PointStatus.ReadyToApply){                                            
                        brushLineContext.Canvas.CanvasCameraController.CommandBuffer.DrawMesh(mesh, matrix, point.Value.Material);
                        point.Value.Status = PointStatus.CopiedToCanvas;                                                         
                        i++;
                    } else if (point.Value.Status == PointStatus.Temporary && tempCB != null){                           
                        tempCB.DrawMesh(mesh, matrix, point.Value.Material);                        
                        j++;
                    }
                }
                point = point.Previous;
            }
            return true;
        }
      
        #endregion

    }
}
