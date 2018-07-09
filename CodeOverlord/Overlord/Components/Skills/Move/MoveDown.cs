using System;
using Prime;
using MoonSharp.Interpreter;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class MoveDown : Move
	{
		public MoveDown() : base("Down", new Point(0, 1))
		{ }
	}
}
