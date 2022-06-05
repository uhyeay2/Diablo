using Diablo.API.Endpoints.PlayerEndpoints;
using Diablo.Domain.Interfaces;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;
using FastEndpoints;
using Moq;
using Shouldly;

namespace Diablo.API.Tests.Endpoints.PlayerEndpoints
{
    [TestFixture]
    public class IsNameTakenTests
    {
        private readonly string _nameNotFound = "NameNotFound";
        private readonly string _nameFound = "NameFound";

        private IsNameTaken _endpoint;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var mockedPlayerDataReader = new Mock<IReadPlayerData>();

            mockedPlayerDataReader.Setup(x => x.IsNameTaken(_nameNotFound)).Returns(false);
            mockedPlayerDataReader.Setup(x => x.IsNameTaken(_nameFound)).Returns(true);

            _endpoint = Factory.Create<IsNameTaken>(mockedPlayerDataReader.Object);
        }

        [Test]
        public async Task Given_NoPlayerFound_Should_ReturnFalse()
        {
            (await _endpoint.ExecuteAsync(new IsNameTakenRequest(_nameNotFound), default)).ShouldBeFalse();
        }

        [Test]
        public async Task Given_PlayerFound_Should_ReturnTrue()
        {
            (await _endpoint.ExecuteAsync(new IsNameTakenRequest(_nameFound), default)).ShouldBeTrue();
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task Given_InvalidInput_Should_ReturnBadRequest(string name)
        {
            var (response, _) = await TestConfig.ApiClient.GETAsync<IsNameTaken, IsNameTakenRequest, object>(new IsNameTakenRequest(name));

            response!.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }
    }
}