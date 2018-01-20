using Prime;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public static class Grid
	{
		public static int Width, Height, TileWidth, TileHeight;

		public static Vector2 PointToWorld(int x, int y)
		{
			return new Vector2(x * TileWidth + TileWidth / 2f, y * TileHeight + TileHeight / 2f);
		}

		public static Vector2 PointToWorld(Point p)
		{
			return PointToWorld(p.X, p.Y);
		}

		public static Point WorldToPoint(Vector2 pos)
		{
			var res = Point.Zero;

			res.X = (int)(pos.X / TileWidth);
			res.Y = (int)(pos.Y / TileHeight);

			return res;
		}
	}
}
