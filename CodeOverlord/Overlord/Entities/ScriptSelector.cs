using Prime;
using Prime.UI;
using Prime.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overlord
{
	public class ScriptSelector : Entity
	{
		private MonsterSpawner m;

		private SelectList<VirtualFile> list;

		public List<VirtualFile> Files
		{
			get
			{
				return list.Items;
			}
		}

		public bool IsVisible
		{
			get
			{
				return list.IsVisible;
			}
			set
			{
				list.IsVisible = value;
			}
		}

		public ScriptSelector(float w, float h, MonsterSpawner m)
		{
			this.list = new SelectList<VirtualFile>(new Vector2(w, h), AnchorPoint.CenterRight);

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
					list.Add(key, value);
				}

				dict[key] = value;
			}
		}

		public override void Initialize()
		{
			base.Initialize();

			this.Scene.AddUI(list);

			list.OnValueChange = (l) =>
			{
				m.Script = list.Selected;
			};
		}
	}
}
