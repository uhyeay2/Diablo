using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Diablo.API.Tests")]

namespace Diablo.API.Endpoints.Routes
{
    internal static class PlayerRoutes
    {
        internal const string CreatePlayer = "/player/create";

        internal const string DoesAnyPlayerExist = "/player/doesAnyPlayerExist";
    }
}
