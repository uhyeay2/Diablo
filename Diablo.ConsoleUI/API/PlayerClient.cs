using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Models.RequestObjects.PlayerRequests;
using Diablo.Domain.Models.ResponseObjects.PlayerResponses;
using Diablo.Domain.Services;
using System.Net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Diablo.ConsoleUI.Tests")]

namespace Diablo.ConsoleUI.API
{
    internal class PlayerClient
    {
        private readonly HttpClient _client;
        private readonly IJsonHandler _jsonHandler;

        public PlayerClient(HttpClient client, IJsonHandler jsonHandler)
        {
            _client = client;
            _jsonHandler = jsonHandler;
        }

        internal async Task<bool> DoesAnyPlayerExist()
        {
            using var api = ApiFactory.StartDisposableApi();

            var response = await _client.GetAsync(ApiPath.GetUrl(PlayerRoutes.DoesAnyPlayerExist));

            if(response.StatusCode == HttpStatusCode.OK)
            {
                if ( Boolean.TryParse( await response.Content.ReadAsStringAsync(), out var result))
                {
                    return result;
                }
                throw new InvalidDataException($"Unable to Parse (bool from) the response for {PlayerRoutes.DoesAnyPlayerExist} - Response from API: {response}");
            }
            throw new ApplicationException($"Unable to receive response for {PlayerRoutes.DoesAnyPlayerExist} - Response from API: {response}");
        }

        internal async Task<CreatePlayerResponse> CreatePlayer(CreatePlayerRequest request)
        {
            using var api = ApiFactory.StartDisposableApi();

            var response = await _client.PostAsync( ApiPath.GetUrl(
                PlayerRoutes.CreatePlayer), _jsonHandler.ConvertToHttpContent(request));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await _jsonHandler.ConvertHttpResponse<CreatePlayerResponse>(response);
            }

            return (CreatePlayerResponse)await _jsonHandler.ConvertToErrorResponse(response);
        }
    }
}
