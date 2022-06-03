using Diablo.ConsoleUI.API;
using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Enums;
using Diablo.Domain.Models.Entities;
using Diablo.Domain.Models.RequestObjects;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;
using GenFu;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diablo.ConsoleUI.Tests.API
{
    [TestFixture]
    public class PlayerClientTests
    {
        private PlayerClient _playerClient = new(new());

        [Test]
        public async Task DoesAnyPlayerExist_Given_NoPlayersExist_Should_ReturnFalse()
        {
            (await _playerClient.DoesAnyPlayerExist()).ShouldBeFalse();
        }

        [Test]
        public async Task DoesAnyPlayerExist_Given_PlayersExist_Should_ReturnTrue()
        {
            (await _playerClient.DoesAnyPlayerExist()).ShouldBeTrue();
        }

        [Test]
        public async Task CreatePlayer_GivenValidRequest_Should_ReturnPlayer()
        {
            var validRequest = new CreatePlayerRequest("Daniel", (PlayerClass)5);

            (await _playerClient.CreatePlayer(validRequest)).Player.Name.ShouldBe("Daniel");
        }
    }
}
