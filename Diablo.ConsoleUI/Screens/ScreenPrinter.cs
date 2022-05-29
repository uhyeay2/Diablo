using Diablo.ConsoleUI.Enums;
using Diablo.ConsoleUI.ExtensionMethods;

namespace Diablo.ConsoleUI.Screens
{
    internal class ScreenPrinter
    {
        private int _sleepTimer = 0;
        private WriteLineStyle _writeLineStyle = WriteLineStyle.Normal;

        private bool _isPrinting = false;
        internal bool IsPrinting => _isPrinting;

        internal void UpdateSettings(int? sleepTimer = null, WriteLineStyle? writeLineStyle = null)
        {
            _sleepTimer = sleepTimer ?? _sleepTimer;
            _writeLineStyle = writeLineStyle ?? _writeLineStyle;
        }

        internal void Print(IEnumerable<string> strings, bool clearBeforePrint = true)
        {
            ClearScreen(clearBeforePrint);

            _isPrinting = true;

            foreach (string s in strings)
            {
                Print(s);
            }

            _isPrinting = false;
        }

        internal void Print(string str, bool clearBeforePrint = false)
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
                        if(c != ' ')
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

        internal void PrintCenteredHorizontally(IEnumerable<string> strings, bool clearScreen = false)
        {
            Print(strings.PadToCenter(), clearScreen);
        }

        internal void PrintCentered(IEnumerable<string> strings)
        {
            CenterVertically(strings.Count());

            PrintCenteredHorizontally(strings);
        }

        private void CenterVertically(int numberOfStringsToPrint)
        {
            ClearScreen();

            var numberOfLinesToSkip = (Console.WindowHeight / 2) - (numberOfStringsToPrint / 2);

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
