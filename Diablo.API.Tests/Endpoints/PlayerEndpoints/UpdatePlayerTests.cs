using Diablo.API.Endpoints.PlayerEndpoints;
using Diablo.Domain.Enums;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models.Entities;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;
using Diablo.Domain.Models.ResponseObjects.PlayerResponses;
using FastEndpoints;
using GenFu;
using Moq;
using Shouldly;

namespace Diablo.API.Tests.Endpoints.PlayerEndpoints
{
    [TestFixture]
    public class UpdatePlayerTests
    {
        private UpdatePlayer _endpoint;
        private Player _player = A.New<Player>();
        private string _playerNameTaken = "PlayerNameTaken";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();
            mockedPlayerDataReader.Setup(x => x.IsNameTaken(_playerNameTaken)).Returns(true);
            
            var mockedPlayerDataWriter = new Mock<IWritePlayerData>();

            _endpoint = Factory.Create<UpdatePlayer>(mockedPlayerDataWriter.Object, mockedPlayerDataReader.Object);
        }

        [Test]
        public async Task Given_InvalidPlayer_Should_ReturnBadRequest()
        {
            var (response, _) = await TestConfig.ApiClient.PUTAsync<UpdatePlayer, UpdatePlayerRequest, UpdatePlayerResponse>
                    (new UpdatePlayerRequest() { Player = null });

            response!.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Given_InvalidPlayerName_Should_ReturnBadRequest(string name)
        {
            var (response, _) = await TestConfig.ApiClient.PUTAsync<UpdatePlayer, UpdatePlayerRequest, UpdatePlayerResponse>
                    (new UpdatePlayerRequest(new (name, (PlayerClass)4)));

            response!.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Give_InvalidPlayerName_Should_ReturnBadRequest2(string name)
        {
            try
            {
                await _endpoint.ExecuteAsync(new UpdatePlayerRequest(new(name, (PlayerClass)3)), default);
            }
            catch (Exception ex)
            {
                
            }
        }

        [Test]
        public async Task Given_NoPlayerFound_Should_ReturnBadRequest()
        {
            var (response, _) = await TestConfig.ApiClient.PUTAsync<UpdatePlayer, UpdatePlayerRequest, UpdatePlayerResponse>(new UpdatePlayerRequest(_player));

            response!.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
