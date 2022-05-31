using Diablo.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Interfaces
{
    public interface IReadPlayerData
    {

        Task<IEnumerable<Player>> GetAllPlayersAsync();

        Task<Player> GetPlayerByNameAsync(string name);

        bool IsNameTaken(string name);

    }
}
