using System;
using System.Threading.Tasks;
using System.Threading;
using Prime;
using Eto.Forms;
using Overlord.Editor;
using CodeOverlord.Overlord.Scenes;

namespace Overlord
{
	/// <summary>
	/// The main class.
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var sla = new LevelSelectionScene();

			using (var game = new OverLordGame(new LevelSelectionScene()))
			{
				Task.Run(() => App.Run(game));

				game.Run();
			}

		}
	}
}
