using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.RequestObjects.PlayerRequests
{
    public class GetPlayerByNameRequest
    {
        public string Name { get; set; } = string.Empty;

        public GetPlayerByNameRequest() { }

        public GetPlayerByNameRequest(string name)
        {
            Name = name;
        }
    }
}
