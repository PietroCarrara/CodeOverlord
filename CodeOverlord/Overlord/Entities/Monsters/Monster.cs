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

		private const float speed = 50;

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

		// Where the player wants us to go
		private Vector2? target;
		private Point targetP;

		public override void Initialize()
		{
			base.Initialize();

			Lua = new LuaInterpreter();

			LuaInterpreter.RegisterType<Vector2>();
			LuaInterpreter.RegisterType<Monster>();

			this.Add(Lua);

			BattleManager.Monsters.Add(this);
		}

		public override void Update()
		{
			base.Update();

			if(this.target != null && this.Position.DistanceBetween(this.target.Value) > 1)
			{
				var movement = Vector2.Zero;
				var target = this.target.Value;

				if(this.Position.X < target.X)
					movement.X = speed;
				else if(this.Position.X > target.X)
					movement.X = -speed;

				if(this.Position.Y < target.Y)
					movement.Y = speed;
				else if(this.Position.Y > target.Y)
					movement.Y = -speed;

				this.Position += movement * Time.DetlaTime;
			}
			else if (target != null)
			{
				this.Pos = targetP;
				target = null;
				BattleManager.Current = null;
			}
		}

		public void Move(int x, int y)
		{
			if(Math.Abs(x + y) > Movement)
				throw new InvalidMoveException("This monster only moves " + this.Movement + " spaces, but you tried moving " + (x + y) + "!");

			var p = this.Pos + new Point(x, y);
			
			this.target = Grid.PointToWorld(p);
			this.targetP = p;

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
