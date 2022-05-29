using Diablo.Domain.Exceptions;
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
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BadRequestException($"Cannot locate player - Invalid name provided. Name received: ({name})");
            }

            throw new NotImplementedException();
        }

        public Task<bool> IsNameTakenAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
