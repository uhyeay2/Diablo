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
        Task<int> CreateNewPlayerAsync(string name, PlayerClass playerClass);

        Task<int> UpdatePlayerAsync(Player player);
    }
}
