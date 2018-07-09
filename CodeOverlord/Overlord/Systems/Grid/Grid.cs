using Prime;
using Prime.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public static class Grid
	{
		public static int Width, Height;

		private static int tileWidth, tileHeight;

		private static Entity e = new Entity();

		private static LevelScene scene;

		public static int TileWidth
		{
			get
			{
				return tileWidth;
			}
			set
			{
				tileWidth = value;

				HorizontalLines.Width = value * Width;
				HorizontalLines.Origin = new Vector2(HorizontalLines.Width, HorizontalLines.Height) / 2f;
			}
		}

		public static int TileHeight
		{
			get
			{
				return tileHeight;
			}
			set
			{
				tileHeight = value;

				VerticalLines.Height = value * Height;
				// VerticalLines.Origin = new Vector2(VerticalLines.Width, VerticalLines.Height) / 2f;
			}
		}

		private static Sprite VerticalLines = new RectangleSprite(3, 1, Color.Red), HorizontalLines = new RectangleSprite(1, 3, Color.Blue);

		public static bool IsAvailable(Point p)
		{
			foreach(var c in BattleManager.Participants)
			{
				if (c.GridPos == p)
					return false;
			}

			return true;
		}

		public static bool IsAvailable(int x, int y)
		{
			return IsAvailable(new Point(x, y));
		}

		public static void Init(LevelScene s)
		{
			scene = s;

			s.Add(e);

			e.Add(VerticalLines);
			e.Add(HorizontalLines);
		}

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

			res.X = (int) Math.Floor((pos.X / TileWidth));
			res.Y = (int) Math.Floor((pos.Y / TileHeight));

			return res;
		}
	}
}
