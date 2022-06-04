using Diablo.Domain.Models.ResponseObjects;
using Diablo.Domain.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Diablo.Services
{
    public class JsonHandler : IJsonHandler
    {
        private const string RequestObjectMediaType = "application/json";

        public async Task<T> ConvertHttpResponse<T>(HttpResponseMessage response)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                throw new InvalidCastException(
                    $"Unable to convert HttpResponse. Target Object: {typeof(T)} - Response: {response}", e);
            }
        }

        public HttpContent ConvertToHttpContent(object requestObject)
        {
            try
            {
                return new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8, RequestObjectMediaType);
            }
            catch (Exception e)
            {
                throw new InvalidCastException(
                    $"Unable to convert to HttpContent. RequestObject: {requestObject}", e);
            }
        }

        public T ConvertTo<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                throw new InvalidCastException(
                    $"Unable to convert object. Target object: {typeof(T)} - Json: {json}", e);
            }
        }

        public async Task<BaseResponse> ConvertToErrorResponse(HttpResponseMessage response)
        {
            try
            {
                return new(JObject.Parse(await response.Content.ReadAsStringAsync())["errors"].Select(x => x.ToString()).ToArray());
            }
            catch (Exception e)
            {
                throw new InvalidCastException($"Unable to convert httpResponse into BaseResponse object. Response: {response}", e);
            }
        }
    }
}
