using Diablo.Data.DataAccess.WriteAccess;
using Diablo.Domain.Models.Entities;
using System.Text.Json;

namespace Diablo.Data.Tests.DataAccess.WriteAccess
{
    public class WritePlayerDataTests
    {
        private IWritePlayerData _playerDataWriter = null!;

        private Mock<IReadPlayerData> _mockedPlayerDataReader = null!;

        private const string NameAlreadyTaken = "PlayerNameTaken";

        private static object[] InvalidUpdatePlayerRequests => DataTestConfig.InvalidUpdatePlayerRequest;

        private static object[] InvalidStringInputs => DataTestConfig.InvalidStringInputs;
        
        [SetUp]
        public void SetUp()
        {
            _mockedPlayerDataReader = new Mock<IReadPlayerData>();

            _mockedPlayerDataReader.Setup(x => x.IsNameTaken(NameAlreadyTaken)).Returns(true);
            _mockedPlayerDataReader.Setup(x => x.IsNameTaken(DataTestConfig.TestPlayerName)).Returns(false);

            _playerDataWriter = new WritePlayerData(_mockedPlayerDataReader.Object);
        }

        [TearDown]
        public void OneTimeTearDown() => DataTestConfig.DeleteTestPlayerIfExists();

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

            _mockedPlayerDataReader.Setup(x => x.IsNameTaken(DataTestConfig.TestPlayerName)).Returns(true);

            await _playerDataWriter.UpdatePlayerAsync(player);

            JsonSerializer.Deserialize<Player>(File.ReadAllText(DataTestConfig.TestPlayerDataPath))?
                .Level.ShouldBe(player.Level);
        }
    }
}
