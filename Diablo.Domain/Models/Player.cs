using Diablo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models
{
    public class Player
    {
        public string Name { get; set; } = string.Empty;

        public int Level { get; set; } = 1;

        public PlayerClass PlayerClass { get; set; }
    }
}
