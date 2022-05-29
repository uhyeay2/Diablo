using Diablo.Data.DataAccess.ReadAccess;

namespace Diablo.Data.Tests.DataAccess.ReadAccess
{
    [TestFixture]
    public class ReadPlayerDataTests
    {
        IReadPlayerData _playerDataReader = new ReadPlayerData();

        private static object[] InvalidStringInputs => DataTestConfig.InvalidStringInputs;

        [Test]
        [TestCaseSource(nameof(InvalidStringInputs))]
        public async Task GetPlayerByName_Given_InvalidName_Should_ThrowBadRequestException(string name)
        {
            await Should.ThrowAsync<BadRequestException>(async () => await _playerDataReader.GetPlayerByNameAsync(name));
        }
    }
}
