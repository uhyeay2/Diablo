using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;

namespace Diablo.API.Endpoints.PlayerEndpoints
{
    public class GetPlayerByName : Endpoint<GetPlayerByNameRequest, Player>
    {

        private readonly IReadPlayerData _playerDataReader;

        public GetPlayerByName(IReadPlayerData playerDataReader)
        {
            _playerDataReader = playerDataReader;
        }

        public override void Configure()
        {
            Get(PlayerRoutes.GetPlayerByName);
            AllowAnonymous();
            Description(d => d.Produces(400));
        }

        public override async Task<Player> ExecuteAsync(GetPlayerByNameRequest request, CancellationToken c)
        {
            if(!_playerDataReader.IsNameTaken(request.Name))
            {
                ThrowError(r => r.Name, $"There was no player found for the name: {request.Name}");
            }

            return await _playerDataReader.GetPlayerByNameAsync(request.Name);
        }

        public class GetPlayerByNameRequestValidator : Validator<GetPlayerByNameRequest> {
            public GetPlayerByNameRequestValidator() {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Player name cannot be empty!");
            }
        }

    }
}
