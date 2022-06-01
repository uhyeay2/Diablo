using Diablo.Domain.Constants.Routes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("Diablo.ConsoleUI.Tests")]

namespace Diablo.ConsoleUI.API
{
    internal class PlayerClient
    {
        private HttpClient _client = new();

        internal async Task<bool> DoesAnyPlayerExist()
        {      
            //var response = await _client.GetAsync(ApiPath.GetUrl(PlayerRoutes.DoesAnyPlayerExist));

            var request = new HttpRequestMessage(HttpMethod.Get, ApiPath.GetUrl(PlayerRoutes.DoesAnyPlayerExist)) { Version = new Version(2,0)};

            var response = await _client.SendAsync(request);

            return response != null;
        }


    }
}
