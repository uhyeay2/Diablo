using Diablo.API.Endpoints.PlayerEndpoints;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models.Entities;
using FastEndpoints;
using GenFu;
using Moq;
using Shouldly;

namespace Diablo.API.Tests.Endpoints.PlayerEndpoints
{

    [TestFixture]
    public class DoesAnyPlayerExistTests
    {
        [Test]
        public async Task Given_NoPlayersExist_Should_ReturnFalse()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();
            mockedPlayerDataReader.Setup(x => x.GetAllPlayersAsync()).Returns(Task.FromResult(Enumerable.Empty<Player>()));

            var endpoint = Factory.Create<DoesAnyPlayerExist>(mockedPlayerDataReader.Object);

            (await endpoint.ExecuteAsync(default)).ShouldBeFalse();
        }

        [Test]
        public async Task GivenPlayersExist_Should_ReturnTrue()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();
            mockedPlayerDataReader.Setup(x => x.GetAllPlayersAsync()).Returns(Task.FromResult(A.ListOf<Player>().AsEnumerable()));

            var endpoint = Factory.Create<DoesAnyPlayerExist>(mockedPlayerDataReader.Object);

            (await endpoint.ExecuteAsync(default)).ShouldBeTrue();

        }
    }
}
