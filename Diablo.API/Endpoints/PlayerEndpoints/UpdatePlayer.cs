using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Exceptions;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;
using Diablo.Domain.Models.ResponseObjects.PlayerResponses;

namespace Diablo.API.Endpoints.PlayerEndpoints
{
    public class UpdatePlayer : Endpoint<UpdatePlayerRequest, UpdatePlayerResponse>
    {
        private readonly IWritePlayerData _playerDataWriter;
        private readonly IReadPlayerData _playerDataReader;

        public UpdatePlayer(IWritePlayerData playerDataWriter, IReadPlayerData playerDataReader)
        {
            _playerDataWriter = playerDataWriter;
            _playerDataReader = playerDataReader;
        }

        public override void Configure()
        {
            Put(PlayerRoutes.UpdatePlayer);
            AllowAnonymous();
            Description(endpoint => endpoint.Produces(400));
            Summary(endpoint =>
            {
                endpoint.Summary = "Given a player, update the saved file with all non-null fields provided.";
                endpoint.Responses[200] = "When a request is successful, the updated player is returned.";
                endpoint.Responses[400] = "When given a player that is null, or there is no player found with the name provided, a 400 response is returned.";

                endpoint.ExampleRequest = new UpdatePlayerRequest(new Player()
                {
                    Name = "NameOfExistingPlayer",
                    Level = 30
                });                
            });
        }

        public override async Task<UpdatePlayerResponse> ExecuteAsync(UpdatePlayerRequest request, CancellationToken c)
        {
            if(!_playerDataReader.IsNameTaken(request.Player!.Name))
            {
                ThrowError($"Unable to Update Player - No player was found with the name: {request.Player!.Name}");
            }

            return new UpdatePlayerResponse(await _playerDataReader.GetPlayerByNameAsync(request.Player!.Name));
        }
    }

    public class UpdatePlayerRequestValidator : Validator<UpdatePlayerRequest>{
        public UpdatePlayerRequestValidator()
        {
            RuleFor(x => x.Player).NotNull().WithMessage("Cannot update Player - No player provided!");
            RuleFor(x => x.Player == null ? null : x.Player.Name).NotEmpty().WithMessage("Cannot update player - No player name was provided!");
        }
    }

}
