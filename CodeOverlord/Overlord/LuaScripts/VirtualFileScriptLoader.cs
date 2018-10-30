using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System.Collections.Generic;
using Overlord;
using Microsoft.Xna.Framework.Graphics;
namespace CodeOverlord.Overlord.LuaScripts
{
	public class VirtualFileScriptLoader : ScriptLoaderBase
	{
		public static Dictionary<string, VirtualFile> Files = new Dictionary<string, VirtualFile>();

		public VirtualFileScriptLoader()
		{
			this.ModulePaths = new[] { "?.lua" };
		}

		public override object LoadFile(string file, Table globalContext)
		{
			if (file == "Slime.lua") return "return Slime()";

			return Files[file].Text;
		}

		public override bool ScriptFileExists(string name)
		{
			if (name == "Slime.lua") return true;

			return Files.ContainsKey(name);
		}
	}
}
