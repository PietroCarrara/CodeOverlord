using System;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace CodeOverlord.Overlord.Systems
{
	public class PlayerData
	{
		public int MaxIndex = 0;

		private static PlayerData instance;

		private const string Filename = "Content/user-data.json";

		private PlayerData()
		{ }

		public static PlayerData GetInstance()
		{
			if (instance == null)
			{
				var json = "{}";

				// TODO: Read if file exists
				if (File.Exists(Filename))
				{
					json = File.ReadAllText(Filename);
				}

				instance = JsonConvert.DeserializeObject<PlayerData>(json);
			}

			return instance;
		}

		public void Save()
		{
			var json = JsonConvert.SerializeObject(this);

			Console.WriteLine(json);

			File.WriteAllText(Filename, json, Encoding.UTF8);
		}
	}
}
