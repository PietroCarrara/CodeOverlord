using System;
using System.IO;

namespace Overlord
{
	public static class ScriptIO
	{
		public static string Load(string filename)
		{
			return File.ReadAllText(filename);
		}

		public static string[] GetScripts()
		{
			return Directory.GetFiles("Content/Scripts/");
		}
	}
}
