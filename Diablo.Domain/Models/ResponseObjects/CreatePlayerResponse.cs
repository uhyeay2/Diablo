using Diablo.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.ResponseObjects
{
    public class CreatePlayerResponse
    {
        public Player Player { get; set; }
        public string[] ErrorMessages = Array.Empty<string>();

        public CreatePlayerResponse()
        {
            Player = new Player();
        }

        public CreatePlayerResponse(Player player)
        {
            Player = player;
        }
    }
}
