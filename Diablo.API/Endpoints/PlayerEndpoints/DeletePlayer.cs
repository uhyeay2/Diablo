using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;

namespace Diablo.API.Endpoints.PlayerEndpoints
{
    public class DeletePlayer : Endpoint<DeletePlayerRequest, bool>
    {
        private readonly IWritePlayerData _playerDataWriter;
        private readonly IReadPlayerData _playerDataReader;

        public DeletePlayer(IWritePlayerData playerDataWriter, IReadPlayerData playerDataReader)
        {
            _playerDataWriter = playerDataWriter;
            _playerDataReader = playerDataReader;
        }

        public override void Configure()
        {
            Delete(PlayerRoutes.DeletePlayer);
            AllowAnonymous();
            Description(endpoint => endpoint.Produces(400));
            Summary(endpoint =>
            {
                endpoint.Summary = "Given a valid player name for a player that exists, delete that player.";
                endpoint.Responses[400] = "400 Response is returned when given an invalid name," +
                    " or if the name provided has already been used.";

                endpoint.ExampleRequest = new DeletePlayerRequest()
                {
                    Name = "Daniel"
                };
            });
        }

        public override Task<bool> ExecuteAsync(DeletePlayerRequest request, CancellationToken c)
        {
            if(!_playerDataReader.IsNameTaken(request.Name))
            {
                ThrowError(r => r.Name, $"Cannot delete player - No player was found with the name provided! Name: {request.Name}");
            }

            _playerDataWriter.DeletePlayer(request.Name);

            return Task.FromResult(true);
        }
    }

    public class DeletePlayerRequestValidator : Validator<CreatePlayerRequest>
    {
        public DeletePlayerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Player name cannot be empty!");
        }
    }
}
