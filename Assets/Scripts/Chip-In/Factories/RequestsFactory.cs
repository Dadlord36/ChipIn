using System.Net;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using HttpRequests;
using RestSharp;

namespace Factories
{
    public static class RequestsFactory
    {
        public static RestRequest MultipartRestRequest(IRequestHeaders requestHeaders)
        {
            var request = new RestRequest(ApiCategories.Profile, Method.PUT);
            request.AddHeader(HttpRequestHeader.Accept.ToString(), ApiHelper.JsonMediaTypeHeader);
            request.AddHeader(HttpRequestHeader.ContentType.ToString(), ApiHelper.MultipartFormData);
            request.AddHeaders(requestHeaders.GetRequestHeaders());
            return request;
        }
    }
}