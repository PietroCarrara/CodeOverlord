using Prime;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public static class Grid
	{
		public static int Width, Height, TileWidth, TileHeight;

		public static Vector2 PointToWorld(int x, int y)
		{
			return new Vector2(x * TileWidth - TileWidth / 2, y * TileHeight - TileHeight / 2);
		}

		public static Vector2 PointToWorld(Point p)
		{
			return PointToWorld(p.X, p.Y);
		}
	}
}
