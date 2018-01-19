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

		public static Point WorldToPoint(Vector2 pos)
		{
			var res = Point.Zero;

			for(int i = 1; ; i++)
			{
				if (PointToWorld(0, i).Y > pos.Y)
				{
					res.Y = i - 1;
					break;
				}
			}

			for(int i = 1; ; i++)
			{
				if (PointToWorld(i, 0).X > pos.X)
				{
					res.X = i - 1;
					break;
				}
			}

			return res;
		}
	}
}
