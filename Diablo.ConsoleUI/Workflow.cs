using Diablo.ConsoleUI.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.ConsoleUI
{
    internal static class Workflow
    {
        private readonly static ScreenPrinter _screen = new ScreenPrinter();

        public static void StartApplication()
        {
            _screen.UpdateSettings(45, Enums.WriteLineStyle.SleepPerCharacter);

            _screen.PrintCentered(new string[] {"Diablo","Console Edition v3.0","","Created By Daniel Aguirre","",
            "For best results, it is recommended that you make your console window Full-Screen."});

            Console.ReadKey();
        }

    }
}
