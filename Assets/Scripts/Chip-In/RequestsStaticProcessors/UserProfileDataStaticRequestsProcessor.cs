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
        private const string Tag = nameof(UserProfileDataStaticRequestsProcessor);
        
        public static async Task<BaseRequestProcessor<object, UserProfileResponseModel, IUserProfileDataWebModel>.HttpResponse>
            GetUserProfileData(IRequestHeaders requestHeaders)
        {
            try
            {
                LogUtility.PrintLog(Tag,$"Request Headers: {requestHeaders.GetRequestHeadersAsString()}");
                return await new UserProfileDataGetProcessor(requestHeaders).
                    SendRequest("User profile data was retrieved");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task TryUpdateUserProfileData(IRequestHeaders requestHeaders, IUserProfileDataWebModel requestBodyProvider)
        {
            try
            {
                LogUtility.PrintLog(Tag,$"RequestHeaders: {requestHeaders.GetRequestHeadersAsString()}");
                LogUtility.PrintLog(Tag,$"RequestBody: {JsonConvert.SerializeObject(requestBodyProvider)}");
                var response = await new UserProfileDataPutProcessor(requestHeaders, requestBodyProvider).
                    SendRequest("User profile data was updated");

                LogUtility.PrintLog(Tag,$"Response User Model: {JsonConvert.SerializeObject(response.ResponseModelInterface)}");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<bool> TryChangeUserProfilePassword(IRequestHeaders requestHeaders, 
            IUserProfilePasswordChangeModel requestBodyModel)
        {
            try
            {
                var response = await new UserProfilePasswordChangePutProcessor(requestHeaders, requestBodyModel).
                    SendRequest("User password was changed successfully");
                return response.ResponseModelInterface != null && response.ResponseModelInterface.Success;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static Task<BaseRequestProcessor<IUserGeoLocation, UserProfileDataWebModel, IUserProfileDataWebModel>.HttpResponse> 
            UpdateUserPosition(IRequestHeaders requestHeaders, IUserGeoLocation userGeoLocation)
        {
            return new UserGeoLocationDataPutProcessor(requestHeaders, userGeoLocation).
                SendRequest("User geo location was successfully sent to server");
        }
    }
}