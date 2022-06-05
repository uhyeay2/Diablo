using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.RequestObjects.PlayerRequests
{
    public class IsNameTakenRequest
    {
        public string Name { get; set; } = string.Empty;

        public IsNameTakenRequest() { }

        public IsNameTakenRequest(string name) { Name = name; }
    }
}
