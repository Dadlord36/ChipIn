using System;
using System.Threading.Tasks;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using HttpRequests.RequestsProcessors.PutRequests;
using Newtonsoft.Json;
using UnityEngine;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class UserProfileDataStaticRequestsProcessor
    {
        public static async Task<BaseRequestProcessor<object, UserProfileResponseModel, IUserProfileDataWebModel>.
                HttpResponse>
            GetUserProfileData(IRequestHeaders requestHeaders)
        {
            Debug.Log($"Request Headers: {requestHeaders.GetRequestHeadersAsString()}");
            return await new UserProfileDataGetProcessor(requestHeaders).SendRequest(
                "User profile data was retrieved");
        }

        public static async
            Task UpdateUserProfileData(IRequestHeaders requestHeaders, IUserProfileDataWebModel requestBodyProvider)
        {
            try
            {
                Debug.Log($"RequestHeaders: {requestHeaders.GetRequestHeadersAsString()}");
                Debug.Log($"RequestBody: {JsonConvert.SerializeObject(requestBodyProvider)}");
                var response = await new UserProfileDataPutProcessor(requestHeaders, requestBodyProvider).SendRequest(
                    "User profile data was updated");
           
                Debug.Log($"Response User Model: {JsonConvert.SerializeObject(response.ResponseModelInterface)}");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }

        }

        public static async Task<bool> ChangeUserProfilePassword(IRequestHeaders requestHeaders,
            IUserProfilePasswordChangeModel requestBodyModel)
        {
            try
            {
                var response =
                    await new UserProfilePasswordChangePutProcessor(requestHeaders, requestBodyModel).SendRequest(
                        "User password was changed successfully");
                return response.ResponseModelInterface.Success;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<bool> UserNameDummy(IRequestHeaders requestHeaders)
        {
            try
            {
                var response =
                    await new UserProfilePasswordChangeDummyPutProcessor(requestHeaders, new DummyData("New Name"))
                        .SendRequest(
                            " was  successfully");
                return response.ResponseModelInterface != null && response.ResponseModelInterface.Success;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}