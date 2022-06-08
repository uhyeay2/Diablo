using Diablo.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.ResponseObjects.PlayerResponses
{
    public class UpdatePlayerResponse : BaseResponse
    {
        public Player Player { get; set; } = new();

        public UpdatePlayerResponse() { }

        public UpdatePlayerResponse(Player player) { Player = player; }

        public UpdatePlayerResponse(string[] errorMessages) : base(errorMessages) { }
    }
}
