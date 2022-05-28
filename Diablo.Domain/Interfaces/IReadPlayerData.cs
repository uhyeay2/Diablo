using Diablo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Interfaces
{
    public interface IReadPlayerData
    {

        Task<Player> GetAllPlayers();

        Task<Player> GetPlayerByName(string name);

        Task<bool> IsNameTaken(string name);

    }
}
