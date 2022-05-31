namespace Diablo.API.Endpoints.PlayerEndpoints
{
    public class DoesAnyPlayerExist : EndpointWithoutRequest<bool>
    {
        private readonly IReadPlayerData _playerDataReader;

        public DoesAnyPlayerExist(IReadPlayerData playerDataReader)
        {
            _playerDataReader = playerDataReader;   
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes(PlayerRoutes.DoesAnyPlayerExist);
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Returns true if any players have been created, or false if none exist";                
                s.Responses[200] = "Returns true if a player has been created, and false if one has not.";
            });
        }

        public override async Task<bool> ExecuteAsync(CancellationToken c)
        {
            return (await _playerDataReader.GetAllPlayersAsync()).Any();
        }

        //public override async Task HandleAsync(CancellationToken c)
        //{
        //    await SendAsync((await _playerDataReader.GetAllPlayersAsync()).Any(), cancellation: c);
        //}
    }
}
