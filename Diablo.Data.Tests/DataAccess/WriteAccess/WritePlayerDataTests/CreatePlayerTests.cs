using Diablo.Data.DataAccess.WriteAccess;

namespace Diablo.Data.Tests.DataAccess.WriteAccess.WritePlayerDataTests
{
    [TestFixture]
    public class CreatePlayerTests
    {
        IWritePlayerData _playerDataWriter = new 
            WritePlayerData(WritePlayerDataTestConfig.GetMockedPlayerDataReaderObject());

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (File.Exists(WritePlayerDataTestConfig.TestPlayerDataPath))
            {
                File.Delete(WritePlayerDataTestConfig.TestPlayerDataPath);
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
                CreateNewPlayerAsync(WritePlayerDataTestConfig.NameAlreadyTaken, PlayerClass.Druid));
        }

        [Test]
        public async Task CreateNewPlayer_Given_NameNotTaken_Should_CreateNewTextFile()
        {
            await _playerDataWriter.CreateNewPlayerAsync(WritePlayerDataTestConfig.TestPlayerName, PlayerClass.Druid);

            File.Exists(WritePlayerDataTestConfig.TestPlayerDataPath).ShouldBeTrue();
        }
    }
}