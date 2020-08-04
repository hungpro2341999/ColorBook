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

namespace PaintCraft.Editor
{
	public static class MenuLinks
	{
		[UnityEditor.MenuItem("Window/PaintCraft/Documentation", false, 110)]
		public static void OpenDocsPortal()
		{
			string url= "http://docs.paintcraft.in/";
			Application.OpenURL(url);
		}

		[UnityEditor.MenuItem("Window/PaintCraft/Video Tutorials", false, 120)]
		public static void OpenVideoTutorials()
		{
			string url= "https://www.youtube.com/playlist?list=PLGeCxyvL-w7TeR1wJTirbFSVMXgslN12C";
			Application.OpenURL(url);
		}

		[UnityEditor.MenuItem("Window/PaintCraft/Support/Forum", false, 130)]
		public static void OpenSupportForum()
		{
			string url= "https://forum.unity.com/threads/released-paintcraft-multiplatform-coloring-book-drawing-app-constructor.404998/";
			Application.OpenURL(url);
		}
		
		[UnityEditor.MenuItem("Window/PaintCraft/Support/Mail", false, 140)]
		public static void OpenSupportMail()
		{
			string url= "mailto:support@paintcraft.in";
			Application.OpenURL(url);
		}		
	}
}
