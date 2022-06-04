using Diablo.Domain.Models.ResponseObjects;
namespace Diablo.Domain.Services
{
    public interface IJsonHandler
    {
        Task<T> ConvertHttpResponse<T>(HttpResponseMessage response);

        HttpContent ConvertToHttpContent(object requestObject);

        T ConvertTo<T>(string json);

        Task<BaseResponse> ConvertToErrorResponse(HttpResponseMessage response);
    }
}
