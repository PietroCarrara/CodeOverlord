using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using CodeOverlord.Overlord.Systems;

namespace Overlord.Overlord.Scenes
{
	public class Level
	{
		private Table level;

		public string Name { get; private set; }

		public int Index { get; private set; }

		// Path to the map file
		public string Map;

		// Files provided by this level
		public Dictionary<string, VirtualFile> Files = new Dictionary<string, VirtualFile>();

		public Dialog Dialog = new Dialog();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Overlord.Overlord.Scenes.Level"/> class.
		/// </summary>
		/// <param name="path">Full path to the .lua file.</param>
		public Level(string path)
		{
			var scriptContent = ScriptIO.Load(path);
			var script = new LevelLua();
			level = script.DoString(scriptContent).Table;

			this.Name = level.Get("name").String;
			this.Map = level.Get("map").String;
			this.Index = (int)level.Get("index").Number;

			var root = path.Substring(0, path.LastIndexOf('/') + 1);

			foreach (var s in level.Get("files").Table.Pairs)
			{
				var table = s.Value.Table;
				var filePath = table.Get("path").String;

				var content = ScriptIO.Load(root + filePath);

				var file = new VirtualFile(content);

				if (table.Get("readOnly").Boolean)
				{
					file.ReadOnly = true;
				}

				if (table.Get("spawnable").Boolean)
				{
					file.Spawnable = true;
				}

				this.Files[filePath] = file;
			}

			foreach (var line in level.Get("dialog").Table.Values)
			{
				var character = line.Table.Get("char").String;
				var contents = line.Table.Get("contents").String;

				Dialog.Put(new Line(character, contents));
			}
		}

		// Level has finished loading
		public void Ready()
		{
			var f = level.Get("ready").Function;
			if (f != null) f.Call();
		}

		// Can use the apis
		public void Initialize()
		{
			var f = level.Get("initialize").Function;
			try
			{
				if (f != null) f.Call();
			}
			catch (ScriptRuntimeException e)
			{
				LuaErrorHandler.Handle(e);
			}
		}

		public string Update()
		{
			var func = level.Get("update");

			if (func.Type == DataType.Function)
			{
				DynValue res = null;
				try
				{
					res = func.Function.Call();
				}
				catch (InterpreterException e)
				{
					LuaErrorHandler.Handle(e);
					return "lose";
				}

				if (!res.IsNil()) return res.String;
			}

			return "";
		}
	}
}
