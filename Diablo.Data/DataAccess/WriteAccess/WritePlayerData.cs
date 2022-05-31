using Diablo.Domain.Enums;
using Diablo.Domain.Exceptions;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models.Entities;
using System.Text.Json;

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

            if ( _playerDataReader.IsNameTaken(name))
            {
                throw new NameAlreadyTakenException($"The name provided ({name}) has already been taken.");
            }

            await SaveData(Paths.SpecificPlayer(name), new Player(name, playerClass));
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            if (player == null || string.IsNullOrWhiteSpace(player.Name))
            {
                throw new BadRequestException("Cannot Update Player - " + player == null ?
                    "Player received was null." : $"Player received did not have a valid name - Name: ({player!.Name})");
            }

            if(!_playerDataReader.IsNameTaken(player.Name))
            {
                throw new PlayerNotFoundException($"No player was found with the name {player.Name}");
            }

            await SaveData(Paths.SpecificPlayer(player.Name), player);
        }

        private async Task SaveData(string path, object obj) => 
            await File.WriteAllTextAsync(path, JsonSerializer.Serialize(obj));        
    }
}
