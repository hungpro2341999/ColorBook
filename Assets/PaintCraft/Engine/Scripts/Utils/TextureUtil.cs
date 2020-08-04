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


namespace PaintCraft.Utils
{
	public class TextureUtil {
		
		public static RenderTexture SetupRenderTextureOnMaterial(Material mat, float width, float height){			
			mat.mainTexture = CreateRenderTexture(width, height);		
			return mat.mainTexture as RenderTexture;
		}

		public static RenderTexture CreateRenderTexture(float width, float height){
			RenderTexture result = new RenderTexture(Mathf.CeilToInt(width), Mathf.CeilToInt(height), 0, RenderTextureFormat.ARGB32);
            result.filterMode = FilterMode.Point;
            return result;
		}

        public static RenderTexture UpdateRenderTextureSize(RenderTexture renderTexture, float width, float height){
            RenderTexture result;
            if (renderTexture.width != Mathf.CeilToInt(width) || renderTexture.height != Mathf.CeilToInt(height)){
                renderTexture.Release();
                result = new RenderTexture(Mathf.CeilToInt(width), Mathf.CeilToInt(height), 0, RenderTextureFormat.ARGB32);
                result.filterMode = FilterMode.Point;
                return result;
            } else {
                return renderTexture;
            }
        }
	}
}
