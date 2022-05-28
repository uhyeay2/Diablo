using Diablo.Data.DataAccess.ReadAccess;
using Diablo.Data.DataAccess.WriteAccess;
using Diablo.Domain.Enums;
using Diablo.Domain.Exceptions;
using Diablo.Domain.Interfaces;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Data.Tests.DataAccess.WriteAccess.Player
{
    [TestFixture]
    public class CreatePlayerTests
    {
        IWritePlayerData _playerDataWriter = null!;

        private const string PlayerNameThatHasBeenTaken = "PlayerNameTaken";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();

            mockedPlayerDataReader.Setup(x => 
                x.IsNameTakenAsync(PlayerNameThatHasBeenTaken)).Returns(Task.FromResult(true));

            _playerDataWriter = new WritePlayerData(mockedPlayerDataReader.Object);
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
            await Should.ThrowAsync<NameAlreadyTakenException>(async () => 
                await _playerDataWriter.CreateNewPlayerAsync(PlayerNameThatHasBeenTaken, PlayerClass.Druid));
        }

    }
}
