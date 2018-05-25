using System;
using System.Threading.Tasks;
using System.Threading;
using Prime;
using Eto.Forms;
using Overlord.Editor;

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
            var game = 	new OverLordGame(
							new LevelScene(ScriptIO.Load("Content/Scripts/Levels/basic-room/basic-room.lua"),
													 	 "Content/Scripts/Levels/basic-room/"));

			App.Game = game;

			var gameT = new Task(game.Run);
			gameT.Start();

			App.app = new Application();
			App.form = new MainForm();

			App.app.Run(App.form);

			// FIXME: Make it possible to quit the game without killing the process
			gameT.Wait();

			// Finish()
        }
    }
}
