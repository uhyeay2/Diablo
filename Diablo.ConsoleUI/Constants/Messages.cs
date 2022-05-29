using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.ConsoleUI.Constants
{
    internal static class Messages
    {
        internal static IEnumerable<string> Get(string messagesKey) => MessageDictionary[messagesKey];

        internal static string Introduction = "IntroductionScreenMessages";

        private static Dictionary<string, IEnumerable<string>> MessageDictionary = new Dictionary<string, IEnumerable<string>>()
        {
            { Introduction, new string[] {
                "Diablo", "Console Edition v3.0", "",
                "Created By Daniel Aguirre","",
                "For the best results, it is recommended that you make your console window Full-Screen.",
                "","Press any key to continue."
                }
            }
        };
    }
}
