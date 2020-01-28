using System.Threading.Tasks;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using HttpRequests.RequestsProcessors.PutRequests;
using Newtonsoft.Json;
using UnityEngine;

namespace RequestsStaticProcessors
{
    public static class UserProfileDataStaticRequestsProcessor
    {
        public static async Task<BaseRequestProcessor<object, UserProfileResponseModel, IUserProfileDataWebModel>.HttpResponse>
            GetUserProfileData(IRequestHeaders requestHeaders)
        {
            Debug.Log($"Request Headers: {requestHeaders.GetRequestHeadersAsString()}");
            return await new UserProfileDataGetProcessor(requestHeaders).SendRequest(
                "User profile data was retrieved");
        }

        public static async
            Task UpdateUserProfileData(IRequestHeaders requestHeaders, IUserProfileDataWebModel requestBodyProvider)
        {
            Debug.Log($"RequestHeaders: {requestHeaders.GetRequestHeadersAsString()}");
            Debug.Log($"RequestBody: {JsonConvert.SerializeObject(requestBodyProvider)}");
            await new UserProfileDataPutProcessor(requestHeaders, requestBodyProvider).SendRequest(
                "User profile data was updated");
        }
    }
}