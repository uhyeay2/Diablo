using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Constants.Routes
{
    public static class PlayerRoutes
    {
        public const string CreatePlayer = "player/create";

        public const string DoesAnyPlayerExist = "player/doesAnyPlayerExist";

        public const string GetAllPlayers = "player/getAllPlayers";

        public const string GetPlayerByName = "player/getPlayer";

        public const string IsNameTaken = "player/isNameTaken";
    }
}
