using Diablo.Data.DataAccess;
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

        private const string _nameAlreadyTaken = "PlayerNameTaken";

        private const string _testPlayerName = "TestPlayerName";

        private string _testPlayerDataPath => Paths.PlayerData + _testPlayerName;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();

            mockedPlayerDataReader.Setup(x =>
                x.IsNameTakenAsync(_testPlayerName)).Returns(Task.FromResult(false));

            mockedPlayerDataReader.Setup(x => 
                x.IsNameTakenAsync(_nameAlreadyTaken)).Returns(Task.FromResult(true));

            _playerDataWriter = new WritePlayerData(mockedPlayerDataReader.Object);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (File.Exists(_testPlayerDataPath))
            {
                File.Delete(_testPlayerDataPath);
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
            await Should.ThrowAsync<NameAlreadyTakenException>(async () => 
                await _playerDataWriter.CreateNewPlayerAsync(_nameAlreadyTaken, PlayerClass.Druid));
        }

        [Test]
        public async Task CreateNewPlayer_Given_NameNotTaken_Should_CreateNewTextFile()
        {
            await _playerDataWriter.CreateNewPlayerAsync(_testPlayerName, PlayerClass.Druid);

            File.Exists(_testPlayerDataPath).ShouldBeTrue();
        }
    }
}