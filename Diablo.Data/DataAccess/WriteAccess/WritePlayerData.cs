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

            await SaveData($"{Paths.PlayerData}{name}", new Player(name, playerClass));
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

            await SaveData($"{Paths.PlayerData}{player.Name}", player);
        }

        private async Task SaveData(string path, object obj) => 
            await File.WriteAllTextAsync(path, JsonSerializer.Serialize(obj));        
    }
}
