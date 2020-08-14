using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using Factories;
using GlobalVariables;
using HttpRequests;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using HttpRequests.RequestsProcessors.PutRequests;
using RestSharp;
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

        public static Task<IRestResponse> UpdateUserProfileData(CancellationToken cancellationToken, IRequestHeaders requestHeaders,
            KeyValuePair<string, string> property)
        {
            var client = ApiHelper.DefaultRestClient;
            var request = RequestsFactory.MultipartRestRequest(requestHeaders);
            
            request.AddParameter(property.Key, property.Value);
            
            var result = request.ToString();

            LogUtility.PrintLog(Tag, result);
            return client.ExecuteAsync(request, cancellationToken);
        }

        public static Task<IRestResponse> UpdateUserProfileData(CancellationToken cancellationToken, IRequestHeaders requestHeaders,
            IReadOnlyDictionary<string, string> fields, string newAvatarImagePath)
        {
            var client = ApiHelper.DefaultRestClient;
            var request = RequestsFactory.MultipartRestRequest(requestHeaders);
            
            if (!string.IsNullOrEmpty(newAvatarImagePath))
                request.AddFile(MainNames.ModelsPropertiesNames.Avatar, newAvatarImagePath);
            FillRequestParametersWithNameValueCollection(request, fields);

            var result = request.ToString();

            LogUtility.PrintLog(Tag, result);
            return client.ExecuteAsync(request, cancellationToken);
        }

        private static void FillRequestParametersWithNameValueCollection(IRestRequest request, IReadOnlyDictionary<string, string> collection)
        {
            foreach (var field in collection)
            {
                request.AddParameter(field.Key, field.Value);
            }
        }

        private static void FillRequestParametersWithNameValueCollection(IRestRequest request, NameValueCollection collection, string containerName)
        {
            foreach (string fieldName in collection)
            {
                request.AddParameter($"{containerName}[{fieldName}]", collection[fieldName]);
            }
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