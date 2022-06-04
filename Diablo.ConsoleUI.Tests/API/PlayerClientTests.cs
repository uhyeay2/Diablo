using Diablo.ConsoleUI.API;
using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Models.Entities;
using Diablo.Domain.Models.ResponseObjects.PlayerResponses;
using Diablo.Domain.Services;
namespace Diablo.ConsoleUI.Tests.API
{
    [TestFixture]
    public class PlayerClientTests
    {
        private MockHttpMessageHandler _mockedClient = null!;
        private Mock<IJsonHandler> _mockedJsonHandler = new();
        private Player _testPlayer = null!;

        [SetUp]
        public void SetUp()
        {
            _mockedClient = new MockHttpMessageHandler();
            _testPlayer = A.New<Player>();

            _mockedJsonHandler.Setup(x => x.ConvertHttpResponse<CreatePlayerResponse>(It.IsAny<HttpResponseMessage>()))
                .Returns(Task.FromResult(new CreatePlayerResponse(_testPlayer)));
        }

        [Test]
        public async Task DoesAnyPlayerExist_Given_NoPlayersExist_Should_ReturnFalse()
        {
            _mockedClient.When(ApiPath.GetUrl(PlayerRoutes.DoesAnyPlayerExist)).Respond("application/json", "false");

            (await new PlayerClient(_mockedClient.ToHttpClient(), _mockedJsonHandler.Object).DoesAnyPlayerExist()).ShouldBeFalse();
        }

        [Test]
        public async Task DoesAnyPlayerExist_Given_PlayersExist_Should_ReturnTrue()
        {
            _mockedClient.When(ApiPath.GetUrl(PlayerRoutes.DoesAnyPlayerExist)).Respond("application/json", "true");

            (await new PlayerClient(_mockedClient.ToHttpClient(), _mockedJsonHandler.Object).DoesAnyPlayerExist()).ShouldBeTrue();
        }

        [Test]
        public async Task CreatePlayer_GivenValidRequest_Should_ReturnPlayer()
        {
            _mockedClient.When(ApiPath.GetUrl(PlayerRoutes.CreatePlayer)).Respond("application/json", "");

            (await new PlayerClient(_mockedClient.ToHttpClient(), _mockedJsonHandler.Object).CreatePlayer(new())).Player.ShouldBe(_testPlayer);
        }
    }
}