using Diablo.Domain.Exceptions;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models;
using System.Text.Json;

namespace Diablo.Data.DataAccess.ReadAccess
{
    public class ReadPlayerData : IReadPlayerData
    {
        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            var savePaths = Directory.GetFiles(Paths.PlayerData);

            if (savePaths.Any())
            {
                var players = new List<Player>();

                // Select the end of the savePath to obtain the PlayerName
                foreach (var playerName in savePaths.Select(s => s.Split('/').Last()))
                {
                    players.Add(await GetPlayerByNameAsync(playerName));
                }

                return players;

            }
            return Enumerable.Empty<Player>();
        }

        public async Task<Player> GetPlayerByNameAsync(string name)
        {
            ValidateNameParameter(name);

            if (!IsNameTaken(name))
            {
                throw new PlayerNotFoundException($"No player was found with the name: ({name})");
            }

            return JsonSerializer.Deserialize<Player>(await File.ReadAllTextAsync(Paths.SpecificPlayer(name)))!;
        }

        public bool IsNameTaken(string name)
        {
            ValidateNameParameter(name);            

            return File.Exists(Paths.SpecificPlayer(name));
        }

        /// <summary>
        /// Helper method for validating "name" parameter for ReadPlayerData methods.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="BadRequestException"></exception>
        private void ValidateNameParameter(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BadRequestException($"Cannot locate player - Invalid name provided. Name received: ({name})");
            }
        }
    }
}
