using Diablo.Domain.Interfaces;
using Diablo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Data.DataAccess.ReadAccess
{
    public class ReadPlayerData : IReadPlayerData
    {
        public Task<Player> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPlayerByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNameTakenAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
