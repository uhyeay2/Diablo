using Diablo.ConsoleUI.Enums;
using Diablo.ConsoleUI.ExtensionMethods;
using System.Diagnostics;

namespace Diablo.ConsoleUI
{
    internal class ScreenPrinter
    {
        private int _sleepTimer = 0;
        private WriteLineStyle _writeLineStyle = WriteLineStyle.Normal;

        internal void UpdateSettings(int? sleepTimer = null, WriteLineStyle? writeLineStyle = null)
        {
            _sleepTimer = sleepTimer ?? _sleepTimer;
            _writeLineStyle = writeLineStyle ?? _writeLineStyle;
        }

        internal void Print(IEnumerable<string> strings, bool clearBeforePrint = true)
        {
            ClearScreen(clearBeforePrint);

            foreach (string s in strings)
            {
                Print(s);
            }
        }

        internal void PrintCenteredHorizontally(IEnumerable<string> strings, bool clearScreen = false)
        {
            Print(strings.PadToCenter(), clearScreen);
        }

        internal void PrintCentered(IEnumerable<string> strings)
        {
            CenterVertically(strings.Count());

            PrintCenteredHorizontally(strings);
        }

        private void Print(string str, bool clearBeforePrint = false)
        {
            ClearScreen(clearBeforePrint);

            switch (_writeLineStyle)
            {
                case WriteLineStyle.Normal:
                    goto default;

                case WriteLineStyle.SleepPerLine:
                    Thread.Sleep(_sleepTimer);
                    Console.WriteLine(str);
                    break;

                case WriteLineStyle.SleepPerCharacter:
                    foreach (var c in str)
                    {
                        if (c != ' ')
                        {
                            Thread.Sleep(_sleepTimer);
                        }
                        Console.Write(c);
                    }
                    Console.Write("\n");
                    break;

                default:
                    Console.WriteLine(str);
                    break;
            }
        }

        private static void CenterVertically(int numberOfStringsToPrint)
        {
            ClearScreen();

            var numberOfLinesToSkip = Console.WindowHeight / 2 - numberOfStringsToPrint / 2;

            Console.Write(new string('\n', numberOfLinesToSkip));
        }

        private static void ClearScreen(bool clearScreen = true)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
        }
    }
}
