using Diablo.API.Endpoints.PlayerEndpoints;
using Diablo.Domain.Enums;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models.Entities;
using FastEndpoints;
using Moq;
using Shouldly;
using FastEndpoints.Validation;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;

namespace Diablo.API.Tests.Endpoints.PlayerEndpoints
{
    [TestFixture]
    public class GetPlayerByNameTests
    {

        private readonly string _nameNotFound = "NameNotFound";
        private readonly string _nameFound = "NameFound";

        private GetPlayerByName _endpoint;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();

            mockedPlayerDataReader.Setup(x => x.IsNameTaken(_nameNotFound)).Returns(false);
            mockedPlayerDataReader.Setup(x => x.IsNameTaken(_nameFound)).Returns(true);
            mockedPlayerDataReader.Setup(x => x.GetPlayerByNameAsync(_nameFound)).Returns(Task.FromResult(new Player(_nameFound, (PlayerClass)5)));

            _endpoint = Factory.Create<GetPlayerByName>(mockedPlayerDataReader.Object);
        }

        [Test]
        public async Task Given_PlayerIsFound_Should_ReturnPlayer()
        {
            (await _endpoint.ExecuteAsync(new GetPlayerByNameRequest(_nameFound), default)).Name.ShouldBe(_nameFound);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task Given_InvalidInput_Should_ReturnBadRequest(string name)
        {
            var (response, _) = await TestConfig.ApiClient.GETAsync<GetPlayerByName, GetPlayerByNameRequest, Player>(new GetPlayerByNameRequest(name));

            response!.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Given_NoPlayerIsFound_Should_ReturnBadRequest()
        {
            try
            {
                await _endpoint.ExecuteAsync(new GetPlayerByNameRequest(_nameFound), default);
            }
            catch (ValidationFailureException ex)
            {
                ex.InnerException?.Message.ShouldBe($"The name '{_nameFound}' has already been used.");
            }
        }
    }
}
