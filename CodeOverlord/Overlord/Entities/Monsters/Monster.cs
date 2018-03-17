using Prime;
using Prime.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Overlord
{
	public class Monster : Entity
	{
		public int Stamina, Damage, Health, Reach;
		public int CurrentStamina, CurrentDamage, CurrentHealth, CurrentReach;

		public SpriteSheet Animations;

		private const float speed = 50;

		public Point GridPos
		{
			get
			{
				return Grid.WorldToPoint(this.Position);
			}
			internal set
			{
				this.Position = Grid.PointToWorld(value);
			}
		}

		public LuaInterpreter Lua;

		public override void Initialize()
		{
			base.Initialize();

			// These should be set on the child class
			CurrentStamina = Stamina;
			CurrentDamage = Damage;
			CurrentHealth = Health;
			CurrentReach = Reach;

			Lua = new LuaInterpreter();

			LuaInterpreter.RegisterType<Vector2>();
			LuaInterpreter.RegisterType<Monster>();

			this.Add(Lua);

			// Basic skills
			this.Add(new MoveUp());
			this.Add(new MoveDown());
			this.Add(new MoveLeft());
			this.Add(new MoveRight());
			this.Add(new AttackUp());
			this.Add(new AttackDown());
			this.Add(new AttackLeft());
			this.Add(new AttackRight());
		}

		public override void OnDestroy()
		{
			BattleManager.Remove(this);
		}

		public void ReceiveTurn()
		{
			this.CurrentStamina = this.Stamina;
		}

		public void ReceiveDamage(int d, Action a = null)
		{
			this.CurrentHealth -= d;

			if(this.CurrentHealth <= 0)
			{
				Animations.Play("die", () => 
				{
					Destroy();
					System.Console.WriteLine("Ayyy");
					a?.Invoke();
				});
			}
			else if (a != null)
			{
				a();
			}
		}
	}
}
