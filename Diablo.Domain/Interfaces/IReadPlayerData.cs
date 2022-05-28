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

        Task<Player> GetAllPlayersAsync();

        Task<Player> GetPlayerByNameAsync(string name);

        Task<bool> IsNameTakenAsync(string name);

    }
}
