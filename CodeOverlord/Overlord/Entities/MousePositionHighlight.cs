using Prime;
using Prime.Graphics;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public class MousePositionHighlight : Entity
	{
		private Point size;

		private Sprite spr;

		public MousePositionHighlight(int width, int height)
		{
			size = new Point(width, height);
		}

		public override void Initialize()
		{
			this.spr = new RectangleSprite(size.X, size.Y, Color.AliceBlue);

			this.Add(spr);
		}

		public override void Update()
		{
			// Position itsef on one of the tiles
			this.Position = Grid.PointToWorld( Grid.WorldToPoint( Input.MousePosition(this.Scene.Cam)));
		}
	}
}
