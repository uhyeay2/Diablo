using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.ConsoleUI.ExtensionMethods
{
    internal static class StringArrayExtensions
    {

        internal static IEnumerable<string> PadToCenter(this IEnumerable<string> strings) => strings.Select(s => s.PadToCenter());

        internal static IEnumerable<string> PadToCenter(this IEnumerable<string> strings, int targetLength) =>
            strings.Select(s => s.PadToCenter(targetLength));
    }
}
