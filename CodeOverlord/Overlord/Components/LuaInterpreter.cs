using System;
using Prime;
using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;

namespace Overlord
{
	public class LuaInterpreter : Component
	{
		public Script Script = new Script();

		public string Content = "";

		public override void Initialize()
		{
			Script.Globals["this"] = this.Owner;
		}

		public override void Update()
		{
			Script.DoString(Content);
		}
	}
}
