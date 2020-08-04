/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using UnityEngine;

namespace PaintCraft.Tools{
	[Serializable]
	public class LineConfig : MonoBehaviour {
        public Brush      Brush;
        public PointColor Color = PointColor.White;	
        public float      Spacing = 1.0f;
        [Range(0.0f,1.0f)]
		public float	  Scale  = 1.0f;
        public Texture    Texture;

        void Start(){
            if (Brush == null){
                Debug.LogError("you must provide default Brush tool here ", gameObject);
            }
        }
	}
}
