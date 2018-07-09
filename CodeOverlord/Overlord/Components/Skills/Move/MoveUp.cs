using System;
using Prime;
using MoonSharp.Interpreter;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class MoveUp : Move
	{
		public MoveUp() : base("Up", new Point(0, -1))
		{ }
	}
}
