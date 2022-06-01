using Diablo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.RequestObjects
{
    public class CreatePlayerRequest
    {
        public CreatePlayerRequest() { }

        public CreatePlayerRequest(string name, PlayerClass playerClass)
        {
            Name = name;
            PlayerClass = playerClass;
        }

        public string Name { get; set; } = string.Empty;
        public PlayerClass PlayerClass { get; set; }
    }
}
