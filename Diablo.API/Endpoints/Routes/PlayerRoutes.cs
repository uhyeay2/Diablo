using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Diablo.API.Tests")]

namespace Diablo.API.Endpoints.Routes
{
    internal static class PlayerRoutes
    {
        internal const string CreatePlayer = "player/create";

        internal const string DoesAnyPlayerExist = "player/doesAnyPlayerExist";

        internal const string GetAllPlayers = "player/getAllPlayers";

        internal const string GetPlayerByName = "player/getPlayer";
    }
}
