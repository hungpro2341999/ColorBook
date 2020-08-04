/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/



namespace PaintCraft.Editor
{

	public static class OpenLocalCacheFolder
	{
		public static bool IsInMacOS
		{
			get { return UnityEngine.SystemInfo.operatingSystem.IndexOf("Mac OS") != -1; }
		}

		public static bool IsInWinOS
		{
			get { return UnityEngine.SystemInfo.operatingSystem.IndexOf("Windows") != -1; }
		}

		[UnityEditor.MenuItem("Window/PaintCraft/OpenLocalCacheFolder", false, 2)]
		public static void OpenPersistentDataPath()
		{
			Open(UnityEngine.Application.persistentDataPath);
		}

		private static void OpenInMac(string path)
		{
			bool openInsidesOfFolder = false;

			// try mac
			string macPath = path.Replace("\\", "/"); // mac finder doesn't like backward slashes

			if (System.IO.Directory.Exists(macPath)) // if path requested is a folder, automatically open insides of that folder
			{
				openInsidesOfFolder = true;
			}

			if (!macPath.StartsWith("\""))
			{
				macPath = "\"" + macPath;
			}

			if (!macPath.EndsWith("\""))
			{
				macPath = macPath + "\"";
			}

			string arguments = (openInsidesOfFolder ? "" : "-R ") + macPath;

			try
			{
				System.Diagnostics.Process.Start("open", arguments);
			}
			catch (System.ComponentModel.Win32Exception e)
			{
				// tried to open mac finder in windows
				// just silently skip error
				// we currently have no platform define for the current OS we are in, so we resort to this
				e.HelpLink = ""; // do anything with this variable to silence warning about not using it
			}
		}

		private static void OpenInWin(string path)
		{
			bool openInsidesOfFolder = false;

			// try windows
			string winPath = path.Replace("/", "\\"); // windows explorer doesn't like forward slashes

			if (System.IO.Directory.Exists(winPath)) // if path requested is a folder, automatically open insides of that folder
			{
				openInsidesOfFolder = true;
			}

			try
			{
				System.Diagnostics.Process.Start("explorer.exe", (openInsidesOfFolder ? "/root," : "/select,") + winPath);
			}
			catch (System.ComponentModel.Win32Exception e)
			{
				// tried to open win explorer in mac
				// just silently skip error
				// we currently have no platform define for the current OS we are in, so we resort to this
				e.HelpLink = ""; // do anything with this variable to silence warning about not using it
			}
		}

		private static void Open(string path)
		{
			if (IsInWinOS)
			{
				OpenInWin(path);
			}
			else if (IsInMacOS)
			{
				OpenInMac(path);
			}
			else // couldn't determine OS
			{
				OpenInWin(path);
				OpenInMac(path);
			}
		}
	}
}
