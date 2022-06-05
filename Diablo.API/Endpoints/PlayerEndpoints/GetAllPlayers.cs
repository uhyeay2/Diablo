using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Models.ResponseObjects.PlayerResponses;

namespace Diablo.API.Endpoints.PlayerEndpoints
{
    public class GetAllPlayers : EndpointWithoutRequest<GetAllPlayersResponse>
    {
        private readonly IReadPlayerData _playerDataReader;
        public GetAllPlayers(IReadPlayerData playerDataReader)
        {
            _playerDataReader = playerDataReader;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes(PlayerRoutes.GetAllPlayers);
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Return all saved players";
            });
        }

        public override async Task<GetAllPlayersResponse> ExecuteAsync(CancellationToken c)
        {
            return new GetAllPlayersResponse() { Players = await _playerDataReader.GetAllPlayersAsync() };
        }

    }
}
