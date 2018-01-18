using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;

namespace Overlord
{
	public class Demo : Entity
	{
		public LuaInterpreter Script;

		public override void Initialize()
		{
			base.Initialize();

			Script = new LuaInterpreter();
			UserData.RegisterType<Demo>();
			UserData.RegisterType<Vector2>();

			Script.Script.Globals["x"] = 0;
			Script.Script.Globals["y"] = 0;
			Script.Content = @"
				y = y + 1
				x = x + 1
				this.Move(x, y)
			";
			this.Add(Script);

			this.Add(new RectangleSprite(200, 200));
		}

		public void Move(float x, float y)
		{
			this.Position = new Vector2(x, y);
		}
	}
}
