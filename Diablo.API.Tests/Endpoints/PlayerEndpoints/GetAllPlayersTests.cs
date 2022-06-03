using Diablo.API.Endpoints.PlayerEndpoints;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models.Entities;
using FastEndpoints;
using GenFu;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.API.Tests.Endpoints.PlayerEndpoints
{
    [TestFixture]
    public class GetAllPlayersTests
    {

        [Test]
        public async Task Given_NoPlayersFound_Should_ReturnEmptyIEnumerable()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();

            mockedPlayerDataReader.Setup(x => x.GetAllPlayersAsync())
                .Returns(Task.FromResult(Enumerable.Empty<Player>()));

            var endpoint = Factory.Create<GetAllPlayers>(mockedPlayerDataReader.Object);

            (await endpoint.ExecuteAsync(default)).Players.ShouldBe(Enumerable.Empty<Player>());
        }

        [Test]
        public async Task Given_PlayersFound_Should_ReturnCorrectCountOfPlayersFound()
        {
            var numberOfPlayers = 5;

            var mockedPlayerDataReader = new Mock<IReadPlayerData>();

            mockedPlayerDataReader.Setup(x => x.GetAllPlayersAsync())
                .Returns(Task.FromResult(A.ListOf<Player>(numberOfPlayers).AsEnumerable()));

            var endpoint = Factory.Create<GetAllPlayers>(mockedPlayerDataReader.Object);
            
            (await endpoint.ExecuteAsync(default)).Players.Count().ShouldBe(numberOfPlayers);
        }

    }
}
