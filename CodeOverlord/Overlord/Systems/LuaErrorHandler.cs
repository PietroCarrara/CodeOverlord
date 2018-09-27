using System;
using MoonSharp.Interpreter;
using Overlord.Editor;
namespace CodeOverlord.Overlord.Systems
{
	public static class LuaErrorHandler
	{
		public static void Handle(InterpreterException e)
		{
			App.Alert(e.DecoratedMessage);
		}
	}
}
