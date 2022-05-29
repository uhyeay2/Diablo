using Diablo.Data.DataAccess.WriteAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diablo.Data.Tests.DataAccess.WriteAccess
{
    public class WritePlayerDataTests
    {
        IWritePlayerData _playerDataWriter = null!;

        Mock<IReadPlayerData> _mockedPlayerDataReader = null!;

        private const string NameAlreadyTaken = "PlayerNameTaken";
        
        private const string PlayerNotFoundName = "UpdatePlayerNotFound";

        #region Setup/Teardown/TestCasesSources

        [SetUp]
        public void SetUp()
        {
            _mockedPlayerDataReader = new Mock<IReadPlayerData>();

            _mockedPlayerDataReader.Setup(x => x.IsNameTakenAsync(NameAlreadyTaken)).Returns(Task.FromResult(true));
            _mockedPlayerDataReader.Setup(x => x.IsNameTakenAsync(PlayerNotFoundName)).Returns(Task.FromResult(false));
            _mockedPlayerDataReader.Setup(x => x.IsNameTakenAsync(DataTestConfig.TestPlayerName)).Returns(Task.FromResult(false));

            _playerDataWriter = new WritePlayerData(_mockedPlayerDataReader.Object);
        }

        [TearDown]
        public void OneTimeTearDown() => DataTestConfig.DeleteTestPlayerIfExists();

        private static object[] InvalidUpdatePlayerRequests => DataTestConfig.InvalidUpdatePlayerRequest;

        private static object[] InvalidStringInputs => DataTestConfig.InvalidStringInputs;

        #endregion

        [Test]
        [TestCaseSource(nameof(InvalidStringInputs))]
        public async Task CreateNewPlayer_Given_InvalidName_Should_ThrowBadRequestException(string name)
        {
            await Should.ThrowAsync<BadRequestException>(async () =>
                await _playerDataWriter.CreateNewPlayerAsync(name, PlayerClass.Druid));
        }

        [Test]
        public async Task CreateNewPlayer_Given_NameAlreadyTaken_Should_ThrowNameAlreadyTakenException()
        {
            await Should.ThrowAsync<NameAlreadyTakenException>(async () => await _playerDataWriter.
                CreateNewPlayerAsync(NameAlreadyTaken, PlayerClass.Druid));
        }

        [Test]
        public async Task CreateNewPlayer_Given_NameNotTaken_Should_CreateNewTextFile()
        {
            await _playerDataWriter.CreateNewPlayerAsync(DataTestConfig.TestPlayerName, PlayerClass.Druid);

            File.Exists(DataTestConfig.TestPlayerDataPath).ShouldBeTrue();
        }

        [Test]
        [TestCaseSource(nameof(InvalidUpdatePlayerRequests))]
        public async Task UpdatePlayer_Given_InvalidPlayer_Should_ThrowBadRequestException(Player player)
        {
            await Should.ThrowAsync<BadRequestException>(async () =>
                await _playerDataWriter.UpdatePlayerAsync(player));
        }

        [Test]
        public async Task UpdatePlayer_Given_NoPlayerFound_Should_ThrowPlayerNotFoundException()
        {
            await Should.ThrowAsync<PlayerNotFoundException>(async () =>
                await _playerDataWriter.UpdatePlayerAsync(new Player(DataTestConfig.TestPlayerName, PlayerClass.Barbarian)));
        }

        [Test]
        public async Task UpdatePlayer_Given_ValidUpdateRequest_Should_UpdatePlayerData()
        {
            var player = new Player(DataTestConfig.TestPlayerName, PlayerClass.Druid) { Level = 30 };

            await _playerDataWriter.CreateNewPlayerAsync(player.Name, player.PlayerClass);
            _mockedPlayerDataReader.Setup(x => x.IsNameTakenAsync(DataTestConfig.TestPlayerName)).Returns(Task.FromResult(true));

            await _playerDataWriter.UpdatePlayerAsync(player);

            JsonSerializer.Deserialize<Player>(File.ReadAllText(DataTestConfig.TestPlayerDataPath))?
                .Level.ShouldBe(player.Level);
        }
    }
}
