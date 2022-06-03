using Diablo.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.ResponseObjects.PlayerResponses
{
    public class CreatePlayerResponse : BaseResponse
    {
        public Player Player { get; set; } = new();

        public CreatePlayerResponse() { }

        public CreatePlayerResponse(Player player) { Player = player; }

        public CreatePlayerResponse(string[] errorMessages) : base(errorMessages) { }
    }
}
