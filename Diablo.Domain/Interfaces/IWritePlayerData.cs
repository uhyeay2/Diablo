using Diablo.Domain.Enums;
using Diablo.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Interfaces
{
    public interface IWritePlayerData
    {
        Task CreateNewPlayerAsync(string name, PlayerClass playerClass);

        Task UpdatePlayerAsync(Player player);

        void DeletePlayer(string name);
    }
}
