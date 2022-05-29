using Diablo.Domain.Enums;
using Diablo.Domain.Exceptions;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diablo.Data.DataAccess.WriteAccess
{
    public class WritePlayerData : IWritePlayerData
    {
        private readonly IReadPlayerData _playerDataReader;

        public WritePlayerData(IReadPlayerData playerDataReader)
        {
            _playerDataReader = playerDataReader;
        }

        public async Task CreateNewPlayerAsync(string name, PlayerClass playerClass)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BadRequestException($"Player cannot be created without a name! Name provided was: {name}");
            }

            if ( await _playerDataReader.IsNameTakenAsync(name))
            {
                throw new NameAlreadyTakenException($"The name provided ({name}) has already been taken.");
            }
            
            var playerData = JsonSerializer.Serialize(new Player(name, playerClass));

            await File.WriteAllTextAsync($"{Paths.PlayerData}{name}", playerData);
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            if (player == null || string.IsNullOrWhiteSpace(player.Name))
            {
                throw new BadRequestException("Cannot Update Player - " + player == null ?
                    "Player received was null." : $"Player received did not have a valid name - Name: ({player!.Name})");
            }

            if(!await _playerDataReader.IsNameTakenAsync(player.Name))
            {
                throw new PlayerNotFoundException($"No player was found with the name {player.Name}");
            }

            throw new NotImplementedException();
        }
    }
}
