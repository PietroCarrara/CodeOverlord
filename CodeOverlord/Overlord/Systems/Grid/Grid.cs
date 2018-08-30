using Prime;
using Prime.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Overlord
{
	public static class Grid
	{
		public static int Width, Height;

		private static Entity e = new Entity();

		private static LevelScene scene;

		private static bool[,] Colliders;

		public static int TileWidth, TileHeight;

		public static bool IsAvailable(Point p)
		{
			if (p.X < 0 || p.Y < 0 || p.X >= Width || p.Y >= Height)
			{
				return false;
			}

			if (Colliders[p.X, p.Y])
				return false;

			foreach (var c in BattleManager.Participants)
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

			Width = scene.Map.WidthInTiles;
			Height = scene.Map.HeightInTiles;
			TileWidth = scene.Map.TileWidth;
			TileHeight = scene.Map.TileHeight;

			Colliders = new bool[Width, Height];

			if (s.Map.Layers.ContainsKey("Colliders"))
			{
				foreach (var p in s.Map.Layers["Colliders"].Objects)
				{
					Colliders[p.X, p.Y] = true;
				}
			}
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
			var res = new Point
			{
				X = (int)Math.Floor(pos.X / TileWidth),
				Y = (int)Math.Floor(pos.Y / TileHeight)
			};

			return res;
		}
	}
}
