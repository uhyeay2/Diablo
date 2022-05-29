using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Data.Tests
{
    internal class DataTestConfig
    {
        internal const string TestPlayerName = "TestPlayerName";

        internal static string TestPlayerDataPath => Paths.PlayerData + TestPlayerName;

        #region TestCaseSources
        
        internal static readonly object[] InvalidStringInputs =
        {
            new object?[] { null },
            new object[] { "" },
            new object[] { " " }
        };

        internal static readonly object[] InvalidUpdatePlayerRequest =
        {
            new object[] { new Player("", PlayerClass.Amazon) },
            new object[] { new Player(" ", PlayerClass.Necromancer) },
            new object[] { new Player("          ", PlayerClass.Druid) }
        };

        #endregion

        internal static void DeleteTestPlayerIfExists()
        {
            if (File.Exists(TestPlayerDataPath))
            {
                File.Delete(TestPlayerDataPath);
            }
        }   
    }
}
