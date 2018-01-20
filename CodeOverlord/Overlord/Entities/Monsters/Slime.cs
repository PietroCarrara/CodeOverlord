using Prime;
using Prime.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

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

			var tex = this.Scene.Content.Load<Texture2D>("Sprites/Monsters/Slime/spritesheet");

			animations = new SpriteSheet(tex, new Point(320, 640), new Point(32));

            animations.Add("idle", 0, 10, 0.1f);
            animations.Add("walk", 20, 30, 0.1f);
            animations.Add("attack", 30, 40, 0.07f);
            animations.Add("die", 40, 50, 0.1f);

			animations.Play("idle");

            animations.Width = Grid.TileWidth;
            animations.Height = Grid.TileHeight;

			Add(animations);

			LuaInterpreter.RegisterType<Slime>();

			Lua.Script.Globals["this"] = this;

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

					if (math.abs(movement.x) + math.abs(movement.y) == 1) then
						this.Attack(movement.x, movement.y)
					else
						
						if(this.Pos.X > target.Pos.X) then
							movement.x = movement.x + 1
						elseif (this.Pos.X < target.Pos.Y) then
							movement.x = movement.x - 1
						elseif (this.Pos.Y < target.Pos.Y) then
							movement.y = movement.y - 1
						elseif (this.Pos.Y > target.Pos.Y) then
							movement.y = movement.y + 1
						end

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
