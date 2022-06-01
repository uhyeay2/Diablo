using Diablo.Domain.Constants.Routes;
using Diablo.Domain.Models.Entities;
using Diablo.Domain.Models.RequestObjects;
using Diablo.Domain.Models.ResponseObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleToAttribute("Diablo.ConsoleUI.Tests")]

namespace Diablo.ConsoleUI.API
{
    internal class PlayerClient
    {
        private HttpClient _client = new();

        internal async Task<bool> DoesAnyPlayerExist()
        {      
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
            var response = await _client.PostAsync(ApiPath.GetUrl(PlayerRoutes.CreatePlayer),
                new StringContent(JsonConvert.SerializeObject(request), UnicodeEncoding.UTF8, "application/json"));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<CreatePlayerResponse>(await response.Content.ReadAsStringAsync());
            }

            return new() 
            { 
                ErrorMessages = JObject.Parse(await response.Content.ReadAsStringAsync())["errors"].Select(x => x.ToString()).ToArray() 
            };
        }

    }
}
