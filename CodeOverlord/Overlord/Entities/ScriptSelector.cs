using Prime;
using Prime.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
	public class ScriptSelector : UIEntity 
	{
		private MonsterSpawner m;

		public ScriptSelector(float w, float h, MonsterSpawner m) : base(w, h)
		{
			this.m = m;
		}

		private Dictionary<string, VirtualFile> dict = new Dictionary<string, VirtualFile>();
		public VirtualFile this[string key]
		{
			get
			{
				return dict[key];
			}
			set
			{
				// If this is the first time,
				// add it to the listview
				if (!dict.ContainsKey(key))
				{
					var text = new TextComponent(key, Content.Fonts.Editor(this.Scene));
					text.Color = Color.Black;

					Scripts.Add(value, text);
				}

				dict[key] = value;
			}
		}

		public ListView<VirtualFile> Scripts;

		public override void Initialize()
		{
			var bg = this.Add(new Sprite(Content.Sprites.UI.Panels.Panel0(this.Scene)));
			bg.Width = this.Width;
			bg.Height = this.Height;
			bg.RelativePosition = new Vector2(0, this.Height) / 2;

			Scripts = this.Insert(new ListView<VirtualFile>(Width, Height, Width, Height / 5));
			Scripts.Position = new Vector2(0, Height / 2);
			Scripts.OnSelected = (s) =>
			{
				this.m.Script = s;
			};
		}
	}
}
