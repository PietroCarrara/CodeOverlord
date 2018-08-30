using System;
using Prime;
using Prime.Graphics;
using System.Collections.Generic;
namespace CodeOverlord.Overlord.Components
{
	public class AnimationScaler : Component
	{
		private SpriteSheet sheet;

		private Dictionary<string, float> originalDurations = new Dictionary<string, float>();

		public override void Initialize()
		{
			base.Initialize();

			this.sheet = this.Owner.GetComponent<SpriteSheet>();

			foreach (var a in this.sheet.Animations)
			{
				this.originalDurations.Add(a.Key, a.Value.FrameDuration);
			}
		}

		public void SetScale(float scale)
		{
			foreach (var a in this.sheet.Animations)
			{
				a.Value.FrameDuration = originalDurations[a.Key] / scale;
			}
		}
	}
}
