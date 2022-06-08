using Diablo.API.Endpoints.PlayerEndpoints;
using Diablo.Data.DataAccess;
using Diablo.Domain.Enums;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models.Entities;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;
using Diablo.Domain.Models.ResponseObjects.PlayerResponses;
using FastEndpoints;
using FastEndpoints.Validation;
using Moq;
using Shouldly;
using System.Net;

namespace Diablo.API.Tests.Endpoints.PlayerEndpoints
{
    [TestFixture]
    public class CreatePlayerTests
    {
        private CreatePlayer _createPlayerEndpoint;

        private readonly string _nameAlreadyTaken = "NameAlreadyTaken";

        private readonly Player _testPlayer = new("TestPlayer", (PlayerClass)1);

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();

            mockedPlayerDataReader.Setup(x => x.IsNameTaken(_nameAlreadyTaken)).Returns(true);

            mockedPlayerDataReader.Setup(x => x.IsNameTaken(_testPlayer.Name)).Returns(false);
            mockedPlayerDataReader.Setup(x => x.GetPlayerByNameAsync(_testPlayer.Name)).Returns(Task.FromResult(_testPlayer));

            _createPlayerEndpoint = Factory.Create<CreatePlayer>(new Mock<IWritePlayerData>().Object, mockedPlayerDataReader.Object);
        }

        [Test]
        public async Task Given_EmptyName_Should_ReturnBadRequest()
        {
            var (response, _) = await TestConfig.ApiClient
                .POSTAsync<CreatePlayer, CreatePlayerRequest, CreatePlayerResponse>(
                    new() { Name = string.Empty, PlayerClass = (PlayerClass)3 });

            response!.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Test]
        [TestCase("1")]
        [TestCase("12")]
        public async Task Given_NameLessThanThreeCharacters_Should_ReturnBadRequest(string name)
        {
            var (response, _) = await TestConfig.ApiClient
                .POSTAsync<CreatePlayer, CreatePlayerRequest, CreatePlayerResponse>(new() { Name = name, PlayerClass = (PlayerClass)2 });

            response!.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }


        [Test]
        [TestCase("123456789012345678901")]
        [TestCase("123456789012345678   23")]
        public async Task Given_NameGreaterThanTwentyCharacters_Should_ReturnBadRequest(string name)
        {
            var (response, _) = await TestConfig.ApiClient
                .POSTAsync<CreatePlayer, CreatePlayerRequest, CreatePlayerResponse>(new() { Name = name, PlayerClass = (PlayerClass)5 });

            response!.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_NameAlreadyTaken_Should_ReturnBadRequest()
        {
            //var (response, _) = await TestConfig.ApiClient
            //.POSTAsync<CreatePlayer, CreatePlayerRequest, CreatePlayerResponse>(new CreatePlayerRequest(_nameAlreadyTaken, (PlayerClass)5));

            //response!.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var response = await _createPlayerEndpoint.ExecuteAsync(new CreatePlayerRequest(_nameAlreadyTaken, (PlayerClass)5), default);

            response.ErrorMessages.ShouldNotBeEmpty();
        }

        [Test]
        public async Task Given_ValidName_Should_ReturnCreatedPlayer()
        {
            var result = await _createPlayerEndpoint.ExecuteAsync(new() { Name = _testPlayer.Name, PlayerClass = _testPlayer.PlayerClass }, default);

            result.Player.ShouldBe(_testPlayer);

            File.Delete(Paths.SpecificPlayer(_testPlayer.Name));
        }
    }
}
