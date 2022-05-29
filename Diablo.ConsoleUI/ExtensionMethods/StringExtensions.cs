using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.ConsoleUI.ExtensionMethods
{
    internal static class StringExtensions
    {
        public static string PadToCenter(this string str, int targetLength)
        {
            if (str.Length > targetLength)
            {
                throw new ArgumentOutOfRangeException($"The string ({str}) cannot be padded to the center of the targetLength. " +
                    $"- the targetLength: ({targetLength}) is shorter than the string currently is ({str} - Length: {str.Length})");
            }

            // formula for centering string to target length
            var leftPaddingSize = (targetLength / 2) + (str.Length / 2);

            return str.PadLeft(leftPaddingSize).PadRight(targetLength);
        }

        public static string PadToCenter(this string str) => PadToCenter(str, Console.WindowWidth);
    }
}
