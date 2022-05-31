using Diablo.Data.DataAccess.ReadAccess;
using Diablo.Domain.Models.Entities;
using GenFu;
using System.Text.Json;

namespace Diablo.Data.Tests.DataAccess.ReadAccess
{
    [TestFixture]
    public class ReadPlayerDataTests
    {
        private readonly IReadPlayerData _playerDataReader = new ReadPlayerData();

        private static object[] InvalidStringInputs => DataTestConfig.InvalidStringInputs;

        [Test]
        [TestCaseSource(nameof(InvalidStringInputs))]
        public async Task GetPlayerByName_Given_InvalidName_Should_ThrowBadRequestException(string name)
        {
            await Should.ThrowAsync<BadRequestException>(async () => await _playerDataReader.GetPlayerByNameAsync(name));
        }

        [Test]
        public async Task GetPlayerByName_Given_NoPlayerFound_Should_ThrowPlayerNotFoundException()
        {
            await Should.ThrowAsync<PlayerNotFoundException>(async () =>
                await _playerDataReader.GetPlayerByNameAsync(DataTestConfig.PlayerNotFoundName));
        }

        [Test]
        public async Task GetPlayerByName_Given_PlayerFoundWithName_Should_ReturnPlayer()
        {
            var player = new Player(DataTestConfig.TestPlayerName, PlayerClass.Druid);

            await File.WriteAllTextAsync(DataTestConfig.TestPlayerDataPath, JsonSerializer.Serialize(player));

            (await _playerDataReader.GetPlayerByNameAsync(player.Name)).Name.ShouldBe(player.Name);
        }

        [Test]
        public async Task GetAllPlayers_Given_NoPlayersFound_Should_ReturnEmptyEnumerable()
        {
            (await _playerDataReader.GetAllPlayersAsync()).ShouldBeEmpty();
        }

        [Test]
        public async Task GetAllPlayers_Given_OnePlayerExists_Should_ReturnThatPlayer()
        {
            var player = new Player(DataTestConfig.TestPlayerName, PlayerClass.Amazon);

            await File.WriteAllTextAsync(DataTestConfig.TestPlayerDataPath, JsonSerializer.Serialize(player));

            (await _playerDataReader.GetAllPlayersAsync()).First().Name.ShouldBe(player.Name);

            DataTestConfig.DeleteTestPlayerIfExists();
        }

        [Test]
        public async Task GetAllPlayers_Given_MultiplePlayersExist_Should_ReturnAllPlayers()
        {
            var players = A.ListOf<Player>(5);

            players.ForEach(p => File.WriteAllText(Paths.SpecificPlayer(p.Name), JsonSerializer.Serialize(p)));

            ( await _playerDataReader.GetAllPlayersAsync()).Count().ShouldBe(players.Count());

            players.ForEach(p => File.Delete(Paths.SpecificPlayer(p.Name)));
        }

        [Test]
        [TestCaseSource(nameof(InvalidStringInputs))]
        public void IsNameTaken_Given_InvalidName_Should_ThrowBadRequestException(string name)
        {
            Should.Throw<BadRequestException>(() => _playerDataReader.IsNameTaken(name));
        }

        [Test]
        public void IsNameTaken_Given_NoFileExistsWithMatchingName_Should_ReturnFalse()
        {
            _playerDataReader.IsNameTaken(DataTestConfig.PlayerNotFoundName).ShouldBeFalse();
        }

        [Test]
        public void IsNameTaken_Given_FileExistsMatchingName_Should_ReturnTrue()
        {
            File.WriteAllText(DataTestConfig.TestPlayerDataPath, "TestFile");
            
            _playerDataReader.IsNameTaken(DataTestConfig.TestPlayerName).ShouldBeTrue();
        }
    }
}
