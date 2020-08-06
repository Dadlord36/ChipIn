using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using HttpRequests.RequestsProcessors.PutRequests;
using Newtonsoft.Json;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class UserProfileDataStaticRequestsProcessor
    {
        private const string Tag = nameof(UserProfileDataStaticRequestsProcessor);

        public static Task<BaseRequestProcessor<object, UserProfileResponseModel, IUserProfileDataWebModel>.HttpResponse>
            GetUserProfileData(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            LogUtility.PrintLog(Tag, $"Request Headers: {requestHeaders.GetRequestHeadersAsString()}");

            return new UserProfileDataGetProcessor(out cancellationTokenSource, requestHeaders).SendRequest(
                "User profile data was retrieved");
        }

        public static Task<BaseRequestProcessor<IUserProfileDataWebModel, UserProfileDataWebModel, IUserProfileDataWebModel>.HttpResponse>
            TryUpdateUserProfileData(out DisposableCancellationTokenSource cancellationTokenSource,
                IRequestHeaders requestHeaders, IUserProfileDataWebModel requestBodyProvider)
        {
            LogUtility.PrintLog(Tag, $"RequestHeaders: {requestHeaders.GetRequestHeadersAsString()}");
            LogUtility.PrintLog(Tag, $"RequestBody: {JsonConvert.SerializeObject(requestBodyProvider)}");
            return new UserProfileDataPutProcessor(out cancellationTokenSource, requestHeaders, requestBodyProvider).SendRequest(
                "User profile data was updated");
        }

        public static Task<BaseRequestProcessor<IUserProfilePasswordChangeModel, UserProfileResponseModel, IUserProfileResponseModel>.HttpResponse> 
            TryChangeUserProfilePassword(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                IUserProfilePasswordChangeModel requestBodyModel)
        {
            return new UserProfilePasswordChangePutProcessor(out cancellationTokenSource, requestHeaders, requestBodyModel)
                .SendRequest("User password was changed successfully");
        }

        public static Task<BaseRequestProcessor<IUserGeoLocation, UserProfileDataWebModel, IUserProfileDataWebModel>.HttpResponse>
            UpdateUserPosition(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                IUserGeoLocation userGeoLocation)
        {
            return new UserGeoLocationDataPutProcessor(out cancellationTokenSource, requestHeaders, userGeoLocation).SendRequest(
                "User geo location was successfully sent to server");
        }
    }
}