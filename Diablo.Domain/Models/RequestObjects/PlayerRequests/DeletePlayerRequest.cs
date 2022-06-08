using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.RequestObjects.PlayerRequests
{
    public class DeletePlayerRequest
    {
        public string Name { get; set; } = string.Empty;

        public DeletePlayerRequest() { }

        public DeletePlayerRequest(string name) { Name = name; }
    }
}
