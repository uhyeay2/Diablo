using Diablo.Data.DataAccess.WriteAccess;

namespace Diablo.Data.Tests.DataAccess.WriteAccess.WritePlayerDataTests
{
    [TestFixture]
    public class CreatePlayerTests
    {
        IWritePlayerData _playerDataWriter = null!;

        private const string NameAlreadyTaken = "PlayerNameTaken";

        private const string TestPlayerName = "TestPlayerName";

        private static string TestPlayerDataPath => Paths.PlayerData + TestPlayerName;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();

            mockedPlayerDataReader.Setup(x =>
                x.IsNameTakenAsync(TestPlayerName)).Returns(Task.FromResult(false));

            mockedPlayerDataReader.Setup(x =>
                x.IsNameTakenAsync(NameAlreadyTaken)).Returns(Task.FromResult(true));

            _playerDataWriter = new WritePlayerData(mockedPlayerDataReader.Object);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (File.Exists(TestPlayerDataPath))
            {
                File.Delete(TestPlayerDataPath);
            }
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
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
            await _playerDataWriter.CreateNewPlayerAsync(TestPlayerName, PlayerClass.Druid);

            File.Exists(TestPlayerDataPath).ShouldBeTrue();
        }
    }
}