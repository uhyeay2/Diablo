using Diablo.ConsoleUI.API;
using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Enums;
using Diablo.Domain.Models.Entities;
using Diablo.Domain.Models.RequestObjects;
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
        private PlayerClient _playerClient = new PlayerClient();
        private Process _apiProcess = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiProcess = Process.Start(ApiPath.Path);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _apiProcess.Close();
        }

        [Test]
        public async Task DoesAnyPlayerExist_Given_NoPlayersExist_Should_ReturnFalse()
        {
            var result = await _playerClient.DoesAnyPlayerExist();

            result.ShouldBeFalse();
        }


        [Test]
        public async Task DoesAnyPlayerExist_Given_PlayersExist_Should_ReturnTrue()
        {
            var result = await _playerClient.DoesAnyPlayerExist();

            result.ShouldBeFalse();
        }


        [Test]
        public async Task CreatePlayer_GivenValidRequest_Should_ReturnPlayer()
        {
            var result = await _playerClient.CreatePlayer(new CreatePlayerRequest("", (PlayerClass)5));

            result.Player.Name.ShouldBe("Daniel");
        }
    }
}
