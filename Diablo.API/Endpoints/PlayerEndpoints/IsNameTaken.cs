using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;

namespace Diablo.API.Endpoints.PlayerEndpoints
{
    public class IsNameTaken : Endpoint<IsNameTakenRequest, bool>
    {
        private readonly IReadPlayerData _playerDataReader;

        public IsNameTaken(IReadPlayerData playerDataReader)
        {
            _playerDataReader = playerDataReader;
        }

        public override void Configure()
        {
            Get(PlayerRoutes.IsNameTaken);
            AllowAnonymous();
            Description(d => d.Produces(400));
        }

        public override Task<bool> ExecuteAsync(IsNameTakenRequest request, CancellationToken c)
        {
            return Task.FromResult(_playerDataReader.IsNameTaken(request.Name));
        }
    }

    public class IsNameTakenRequestValidator : Validator<IsNameTakenRequest>
    {
        public IsNameTakenRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("No name was provided to search for a player by!");
        }
    }
}
