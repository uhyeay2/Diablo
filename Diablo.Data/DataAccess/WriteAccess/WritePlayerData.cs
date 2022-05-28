using Diablo.Domain.Enums;
using Diablo.Domain.Exceptions;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Data.DataAccess.WriteAccess
{
    public class WritePlayerData : IWritePlayerData
    {
        private readonly IReadPlayerData _playerDataReader = null!;
        public WritePlayerData(IReadPlayerData playerDataReader)
        {
            _playerDataReader = playerDataReader;
        }

        public async Task<int> CreateNewPlayerAsync(string name, PlayerClass playerClass)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BadRequestException($"Player cannot be created without a name! Name provided was: {name}");
            }

            if ( await _playerDataReader.IsNameTakenAsync(name))
            {
                throw new NameAlreadyTakenException($"The name provided ({name}) has already been taken.");
            }

            throw new NotImplementedException();
        }

        public Task<int> UpdatePlayerAsync(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
