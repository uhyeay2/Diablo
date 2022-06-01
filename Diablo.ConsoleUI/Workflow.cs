using Diablo.ConsoleUI.API;
using Diablo.ConsoleUI.Constants;
using Diablo.Domain.Constants.Routes;
using System.Diagnostics;

namespace Diablo.ConsoleUI
{
    internal static class Workflow
    {
        private readonly static ScreenPrinter _screen = new();
        
        public static void StartApplication()
        {

            _screen.UpdateSettings(45, Enums.WriteLineStyle.SleepPerCharacter);

            var apiProcess = Process.Start(ApiPath.Path);

            _screen.PrintCentered(Messages.Get(Messages.Introduction));

            Console.ReadKey();

            CharacterSelect();

            apiProcess.Close();
        }

        private static void CharacterSelect()
        {
        }

    }
}
