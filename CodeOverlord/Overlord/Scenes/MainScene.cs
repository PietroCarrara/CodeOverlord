using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class MainScene : Scene
	{
		public override void Initialize()
		{
			base.Initialize();

			Grid.Width = 20;
			Grid.Height = 20;
			Grid.TileWidth = 16;
			Grid.TileHeight = 16;

			var s = new Slime();
			Add(s);

			var s2 = new Slime();
			s2.Pos = new Point(20, 20);
			Add(s2);
		}
	}
}
