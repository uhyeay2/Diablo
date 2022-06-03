using Diablo.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.ResponseObjects.PlayerResponses
{
    public class GetAllPlayersResponse : BaseResponse
    {
        public IEnumerable<Player> Players { get; set; } = Enumerable.Empty<Player>();
    }
}
