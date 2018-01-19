using Prime;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Overlord
{
	public class Monster : Entity
	{
		protected int Movement, Damage, Health, Reach;

		private Point pos;
		public Point Pos
		{
			get
			{
				return pos;
			}
			internal set
			{
				pos = value;
				this.Position = Grid.PointToWorld(pos);
			}
		}

		public LuaInterpreter Lua;


		public override void Initialize()
		{
			base.Initialize();

			Lua = new LuaInterpreter();

			LuaInterpreter.RegisterType<Vector2>();
			LuaInterpreter.RegisterType<Monster>();

			this.Add(Lua);
		}

		public override void Update()
		{
			base.Update();
		}

		public void Move(int x, int y)
		{
			if(Math.Abs(x + y) > Movement)
				throw new InvalidMoveException("This monster only moves " + this.Movement + " spaces, but you tried moving " + (x + y) + "!");

			var p = this.Pos + new Point(x, y);
			base.Position = Grid.PointToWorld(p);
			
			this.Pos = p;

			Lua.IsReady = false;
		}

		public void Attack(int x, int y)
		{
			if(Math.Abs(x + y) > Reach)
				throw new InvalidAttackException("This monster only reaches " + this.Reach + " spaces, but you tried reaching " + (x + y) + "!");

			// TrueAttack();

			Lua.IsReady = false;
		}

	}
}
