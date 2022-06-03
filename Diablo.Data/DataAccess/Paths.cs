using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("Diablo.Data.Tests")]
[assembly: InternalsVisibleToAttribute("Diablo.API.Tests")]
namespace Diablo.Data.DataAccess
{
    internal static class Paths
    {
        static Paths()
        {
            SetBasePathAndCreateDirectories();
        }

        private static string BasePath = String.Empty;
        
        internal static string PlayerData =>  BasePath + "Players/";

        internal static string SpecificPlayer(string playerName) => PlayerData + playerName;

        private static void SetBasePathAndCreateDirectories()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (string.IsNullOrEmpty(directory))
            {
                throw new DirectoryNotFoundException("Was unable to locate BasePath for DataAccess.Paths");
            }

            BasePath = $"{directory}/DiabloConsoleEdition/SaveData/";

            Directory.CreateDirectory(PlayerData);
        }
    }
}
