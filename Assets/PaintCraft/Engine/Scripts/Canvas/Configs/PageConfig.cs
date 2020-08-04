/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.IO;
using UnityEngine;
using PaintCraft.Utils;
using System;

namespace PaintCraft.Canvas.Configs{	
	public abstract class PageConfig : ScriptableObject
	{
	    public string UniqueId;

	    public string IconSavePath
	    {
	        get
	        {
	            string dir = Path.Combine( Application.persistentDataPath , "icons");
	            if (!Directory.Exists(dir))
	            {
	                Directory.CreateDirectory(dir);
	            }
	            return Path.Combine(dir, UniqueId + ".jpg");
	        }
	    }
	    abstract public Vector2 GetSize();


        [TexturePath] public string startImagePath;
        [NonSerialized]
        Texture2D startImageTexture;

        public Texture2D StartImageTexture
        {
            get
            {
                if (startImageTexture == null)
                {                    
                    startImageTexture = Resources.Load<Texture2D>(startImagePath);
                }
                return startImageTexture;
            }
        }
	}
}
