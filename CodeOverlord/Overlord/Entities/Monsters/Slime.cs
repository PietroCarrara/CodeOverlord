using Prime;
using Prime.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class Slime : Monster
	{
		public Slime()
		{
			this.Movement = 3;
			this.Damage = 1;
			this.Health = 2;
			this.Reach = 1;
		}

		public override void Initialize()
		{
			base.Initialize();

			LuaInterpreter.RegisterType<Slime>();

			Lua.Script.Globals["this"] = this;

			this.Add(new RectangleSprite(16, 16, Color.SeaGreen));

			Lua.Content = @"
				function update()
					local target = nil
					
					if (#monsters() > 0) then
						target = monsters()[1]
					end

					if(target == nil) then
						return
					end

					local movement = {}
					movement.x = target.Pos.X - this.Pos.X
 					movement.y = target.Pos.Y - this.Pos.Y

					if (math.abs(movement.x) + math.abs(movement.y) <= 1) then
						this.Attack(movement.x, movement.y)
					else
						while (math.abs(movement.x) + math.abs(movement.y) > 3) do
							if(math.abs(movement.x) > math.abs(movement.y)) then
								if(movement.x > 0) then
									movement.x = movement.x - 1
								else
									movement.x = movement.x + 1
								end
							else
								if(movement.y > 0) then
									movement.y = movement.y - 1
								else
									movement.y = movement.y + 1
								end
							end
						end

						this.Move(movement.x, movement.y)
					end
				end
			";
		}
	}
}
