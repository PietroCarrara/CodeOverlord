using System;

namespace Overlord
{
	public static class TimeControl
	{
		private static float scale = 1;

		public static float GetScale()
		{
			return scale;
		}

		public static void SetScale(float scale)
		{
			TimeControl.scale = scale;
		}
	}
}
