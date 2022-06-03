using Diablo.Domain.Models.ResponseObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.ConsoleUI.API
{
    internal class JsonHandler
    {
        private const string RequestObjectMediaType = "application/json"; 

        internal static async Task<T> ConvertResponse<T>(HttpResponseMessage response)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                throw new InvalidCastException($"Unable to convert HttpResponse. Target Object: {typeof(T)} - Response: {response}", e);
            }
        }

        internal static HttpContent ConvertRequest(object requestObject)
        {
            try
            {
                return new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8, RequestObjectMediaType);
            }
            catch (Exception e)
            {
                throw new InvalidCastException($"Unable to convert to HttpContent. RequestObject: {requestObject}", e);
            }
        }

        internal static async Task<BaseResponse> ConvertErrorResponse(HttpResponseMessage response)
        {
            try
            {
                return new (JObject.Parse(await response.Content.ReadAsStringAsync())["errors"].Select(x => x.ToString()).ToArray());
            }
            catch (Exception e)
            {
                throw new InvalidCastException($"Unable to convert httpResponse into BaseResponse object. Response: {response}", e);
            }
        }
    }
}
