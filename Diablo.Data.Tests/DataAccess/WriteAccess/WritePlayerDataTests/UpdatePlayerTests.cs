using Diablo.Data.DataAccess.WriteAccess;

namespace Diablo.Data.Tests.DataAccess.WriteAccess.WritePlayerDataTests
{
    [TestFixture]
    public class UpdatePlayerTests
    {
        IWritePlayerData _playerDataWriter = new
            WritePlayerData(WritePlayerDataTestConfig.GetMockedPlayerDataReaderObject());

        private static readonly object?[] _invalidUpdatePlayerRequests =
        {
            new object[] { new Player("", PlayerClass.Amazon) },
            new object[] { new Player("  ", PlayerClass.Druid) }
        };  

        [Test]
        [TestCaseSource(nameof(_invalidUpdatePlayerRequests))]
        public async Task UpdatePlayer_Given_InvalidPlayer_Should_ThrowBadRequestException(Player player)
        {
            await Should.ThrowAsync<BadRequestException>(async () =>
                await _playerDataWriter.UpdatePlayerAsync(player));
        }
    }
}
