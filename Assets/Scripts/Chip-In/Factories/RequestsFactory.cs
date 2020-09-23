using System.Net;
using DataModels.HttpRequestsHeadersModels;
using HttpRequests;
using RestSharp;

namespace Factories
{
    public static class RequestsFactory
    {
        public static RestRequest MultipartRestRequest(IRequestHeaders requestHeaders, Method method,in string apiCategory)
        {
            var request = new RestRequest(apiCategory, method);
            request.AddHeader(HttpRequestHeader.Accept.ToString(), ApiHelper.JsonMediaTypeHeader);
            request.AddHeader(HttpRequestHeader.ContentType.ToString(), ApiHelper.MultipartFormData);
            request.AddHeaders(requestHeaders.GetRequestHeaders());
            return request;
        }
    }
}