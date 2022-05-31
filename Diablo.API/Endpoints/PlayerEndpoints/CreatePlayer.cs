namespace Diablo.API.Endpoints.PlayerEndpoints
{
    public class CreatePlayer : Endpoint<CreatePlayerRequest, CreatePlayerResponse>
    {
        private readonly IWritePlayerData _playerDataWriter;
        
        private readonly IReadPlayerData _playerDataReader;

        public CreatePlayer(IWritePlayerData playerDataWriter, IReadPlayerData playerDataReader)
        {
            _playerDataWriter = playerDataWriter;
            _playerDataReader = playerDataReader;
        }

        public override void Configure()
        {
            Post(PlayerRoutes.CreatePlayer);
            AllowAnonymous();
            Description(d => d
                .Produces(400));
            Summary(s =>
            {
                s.Summary = "Given a PlayerClass and a name between 3-20 characters that has not already" +
                    " been used, create a new player.";
                s.ExampleRequest = new CreatePlayerRequest() { Name = "Daniel", PlayerClass = PlayerClass.Druid };
                s.Responses[200] = "When a request is successful, the player created is returned";
                s.Responses[400] = "400 Response is returned when given an invalid name,"+
                    " or if the name provided has already been used.";
            });
        }

        //public override async Task HandleAsync(CreatePlayerRequest request, CancellationToken c)
        //{
        //    if (_playerDataReader.IsNameTaken(request.Name))
        //        ThrowError(r => r.Name, $"The name '{request.Name}' has already been used.");

        //    await _playerDataWriter.CreateNewPlayerAsync(request.Name, request.PlayerClass);

        //    await SendAsync(new CreatePlayerResponse(
        //        await _playerDataReader.GetPlayerByNameAsync(request.Name)), cancellation: c);
        //}

        public override async Task<CreatePlayerResponse> ExecuteAsync(CreatePlayerRequest request, CancellationToken c)
        {
            if (_playerDataReader.IsNameTaken(request.Name))
            {
                ThrowError(r => r.Name, $"The name '{request.Name}' has already been used.");
            }

            await _playerDataWriter.CreateNewPlayerAsync(request.Name, request.PlayerClass);

            return new CreatePlayerResponse(await _playerDataReader.GetPlayerByNameAsync(request.Name)); ;
        }
    }

    public record struct CreatePlayerResponse (Player Player);

    public class CreatePlayerRequestValidator : Validator<CreatePlayerRequest> {
        public CreatePlayerRequestValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Player name cannot be empty!")
                .MinimumLength(3).WithMessage("Name was too short - must be at least three characters!")
                .MaximumLength(20).WithMessage("Name was too long - must be 20 characters or shorter!");
        }
    }
}