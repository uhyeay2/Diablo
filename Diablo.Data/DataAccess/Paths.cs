using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("Diablo.Data.Tests")]
namespace Diablo.Data.DataAccess
{
    internal static class Paths
    {
        private static readonly string BasePath;
        static Paths()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (string.IsNullOrEmpty(directory))
            {
                throw new DirectoryNotFoundException("Was unable to locate BasePath for DataAccess.Paths");
            }

            BasePath = $"{directory}/DiabloConsoleEdition/SaveData/";

            Directory.CreateDirectory(PlayerData);
        }

        internal static string PlayerData =>  BasePath + "Players/";
    }
}
