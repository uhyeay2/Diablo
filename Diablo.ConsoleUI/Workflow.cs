using Diablo.ConsoleUI.Constants;
using Diablo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.ConsoleUI
{
    internal static class Workflow
    {
        private readonly static ScreenPrinter _screen = new();
        
        public static void StartApplication()
        {
            _screen.UpdateSettings(45, Enums.WriteLineStyle.SleepPerCharacter);

            var apiPath = Directory.GetCurrentDirectory();

            //Process.Start(apiPath);

            _screen.PrintCentered(Messages.Get(Messages.Introduction));

            Console.ReadKey();

            CharacterSelect();                            
        }

        private static void CharacterSelect()
        {
        }

    }
}
