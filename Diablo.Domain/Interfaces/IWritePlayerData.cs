using Diablo.Domain.Enums;
using Diablo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Interfaces
{
    public interface IWritePlayerData
    {
        Task<int> CreateNewPlayer(string name, PlayerClass playerClass);

        Task<int> UpdatePlayer(Player player);
    }
}
