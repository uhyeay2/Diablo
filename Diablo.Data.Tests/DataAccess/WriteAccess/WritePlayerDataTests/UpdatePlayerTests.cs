using Diablo.Data.DataAccess.WriteAccess;
using System.Text.Json;

namespace Diablo.Data.Tests.DataAccess.WriteAccess.WritePlayerDataTests
{
    [TestFixture]
    public class UpdatePlayerTests
    {
        IWritePlayerData _playerDataWriter = null!;

        Mock<IReadPlayerData> _mockedPlayerDataReader = null!;

        private const string PlayerNotFoundName = "UpdatePlayerNotFound";

        private const string TestPlayerName = "TestUpdatePlayerName";

        private static string TestPlayerDataPath => Paths.PlayerData + TestPlayerName;

        #region Setup/Teardown/TestCases

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockedPlayerDataReader = new Mock<IReadPlayerData>();

            _mockedPlayerDataReader.Setup(x => x.IsNameTakenAsync(PlayerNotFoundName)).Returns(Task.FromResult(false));
            _mockedPlayerDataReader.Setup(x => x.IsNameTakenAsync(TestPlayerName)).Returns(Task.FromResult(false));

            _playerDataWriter = new WritePlayerData(_mockedPlayerDataReader.Object);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (File.Exists(TestPlayerDataPath))
            {
                File.Delete(TestPlayerDataPath);
            }
        }

        private static readonly object?[] _invalidUpdatePlayerRequests =
        {
            new object[] { new Player("", PlayerClass.Amazon) },
            new object[] { new Player(" ", PlayerClass.Necromancer) },
            new object[] { new Player("          ", PlayerClass.Druid) }
        };

        #endregion

        [Test]
        [TestCaseSource(nameof(_invalidUpdatePlayerRequests))]
        public async Task UpdatePlayer_Given_InvalidPlayer_Should_ThrowBadRequestException(Player player)
        {
            await Should.ThrowAsync<BadRequestException>(async () =>
                await _playerDataWriter.UpdatePlayerAsync(player));
        }

        [Test]
        public async Task UpdatePlayer_Given_NoPlayerFound_Should_ThrowPlayerNotFoundException()
        {
            await Should.ThrowAsync<PlayerNotFoundException>(async () =>
                await _playerDataWriter.UpdatePlayerAsync(new Player(TestPlayerName, PlayerClass.Barbarian)));
        }

        [Test]
        public async Task UpdatePlayer_Given_ValidUpdateRequest_Should_UpdatePlayerData()
        {
            var player = new Player(TestPlayerName, PlayerClass.Druid) { Level = 30 };

            await _playerDataWriter.CreateNewPlayerAsync(player.Name, player.PlayerClass);

            _mockedPlayerDataReader.Setup(x => x.IsNameTakenAsync(TestPlayerName)).Returns(Task.FromResult(true));

            await _playerDataWriter.UpdatePlayerAsync(player);

            var updatedPlayer = JsonSerializer.Deserialize<Player>(File.ReadAllText(TestPlayerDataPath));

            updatedPlayer?.Level.ShouldBe(player.Level);
        }
    }
}
