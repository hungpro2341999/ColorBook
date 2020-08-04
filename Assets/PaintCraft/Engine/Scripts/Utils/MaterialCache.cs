/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections.Generic;
using UnityEngine;
using PaintCraft.Tools;


namespace PaintCraft.Utils{
    public static class MaterialCache {
        const int materialCacheSize = 5;
        static Dictionary<int, Material> materialCache = new Dictionary<int, Material>();
        static Queue<int>  materialAddOrder = new Queue<int>();


        public static Material GetMaterial(BrushContext brushLineContext, Material normalMaterial, Material regionMaterial){            
            if (brushLineContext.Canvas.RegionTexture != null){
                if (regionMaterial != null){                    
                    return GetCachedMaterial(brushLineContext, regionMaterial, brushLineContext.Canvas.RegionTexture);
                } else {
                    Debug.LogError("You didn't specified regionlayer material for this brush");
                    return GetCachedMaterial(brushLineContext, normalMaterial);
                }
            } else {
                return GetCachedMaterial(brushLineContext, normalMaterial);
            }
        }

        static Material GetCachedMaterial(BrushContext brushContext, Material parentMaterial, Texture regionTexture = null){ 
            Material result;
            int cacheId = (parentMaterial.GetInstanceID() * 31
                + (regionTexture == null ? 0 : regionTexture.GetInstanceID()*17) ^ brushContext.LineConfig.GetInstanceID());            
            materialCache.TryGetValue(cacheId, out result);
            if (result == null ){                
                result = new Material(parentMaterial);
                materialCache.Add(cacheId, result);
                materialAddOrder.Enqueue(cacheId);
                if (materialCache.Count > materialCacheSize){
                    materialCache.Remove(materialAddOrder.Dequeue());
                }
            }           

            if (regionTexture != null && (brushContext.IsFirstPointInLine || brushContext.IsLastPointInLine)){ //First and last for brush and bucket. maybe need to avoid this. and set this param every time?                

                result.SetTexture("_RegionTex", regionTexture);
                result.SetFloat("_OriginX", brushContext.FirstPointUVPosition.x);
                result.SetFloat("_OriginY", brushContext.FirstPointUVPosition.y);               
            }

            if (brushContext.ClippingMaskOffset != Vector2.zero && result.HasProperty("_ClippingMask")){
                result.SetTextureOffset("_ClippingMask", brushContext.ClippingMaskOffset );
            }

            return result;
        }
    	
    }
}
