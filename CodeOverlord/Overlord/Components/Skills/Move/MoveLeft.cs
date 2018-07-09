using System;
using Prime;
using MoonSharp.Interpreter;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class MoveLeft : Move
	{
		public MoveLeft() : base("Left", new Point(-1, 0))
		{ }
	}
}
