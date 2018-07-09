using System;
using Prime;
using MoonSharp.Interpreter;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class MoveRight : Move
	{
		public MoveRight() : base("Right", new Point(1, 0))
		{ }
	}
}

