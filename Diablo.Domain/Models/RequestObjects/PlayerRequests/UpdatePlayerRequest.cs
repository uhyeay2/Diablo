using Diablo.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.RequestObjects.PlayerRequests
{
    public class UpdatePlayerRequest
    {
        public Player? Player { get; set; }

        public UpdatePlayerRequest() { }

        public UpdatePlayerRequest(Player player) { Player = player; }
    }
}
