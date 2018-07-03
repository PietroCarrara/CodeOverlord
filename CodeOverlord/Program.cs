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
			using (var game = new OverLordGame(new LevelScene(ScriptIO.Load("Content/Scripts/Levels/basic-room/basic-room.lua"),
													 	 	 				"Content/Scripts/Levels/basic-room/")))
			{
				Task.Run(() => App.Run(game));

				game.Run();
			}

        }
    }
}
