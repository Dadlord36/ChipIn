using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
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
    public static class ProfileDataStaticRequestsProcessor
    {
        private const string Tag = nameof(ProfileDataStaticRequestsProcessor);

        public static Task<BaseRequestProcessor<object, UserProfileResponseModel, IUserProfileModel>.HttpResponse>
            GetUserProfileData(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            LogUtility.PrintLog(Tag, $"Request Headers: {requestHeaders.GetRequestHeadersAsString()}");

            return new UserProfileDataGetProcessor(out cancellationTokenSource, requestHeaders)
                .SendRequest("Profile data was retrieved");
        }

        public static Task<IRestResponse> UpdateUserProfileData(CancellationToken cancellationToken, IRequestHeaders requestHeaders,
            ChangedPropertiesCollector changedPropertiesCollector)
        {
            var request = RequestsFactory.MultipartRestRequest(requestHeaders, Method.PUT, ApiCategories.Profile);
            FillRequestParametersWithNameValueCollection(request, changedPropertiesCollector.ChangedPropertiesCollection);
            FillRequestFilesWithNameValueCollection(request, changedPropertiesCollector.ChangedPropertiesWithFileDataCollection);
            return SendRestRequest(request, cancellationToken);
        }

        public static Task<IRestResponse> UpdateUserProfileData(CancellationToken cancellationToken, IRequestHeaders requestHeaders,
            KeyValuePair<string, string> property)
        {
            var request = RequestsFactory.MultipartRestRequest(requestHeaders, Method.PUT, ApiCategories.Profile);
            request.AddParameter(property.Key, property.Value);

            return SendRestRequest(request, cancellationToken);
        }

        public static Task<IRestResponse> UpdateUserProfileWithDataFile(CancellationToken cancellationToken, IRequestHeaders requestHeaders,
            KeyValuePair<string, string> property)
        {
            var request = RequestsFactory.MultipartRestRequest(requestHeaders, Method.PUT, ApiCategories.Profile);
            request.AddFile(property.Key, property.Value);

            return SendRestRequest(request, cancellationToken);
        }

        public static Task<IRestResponse> UpdateUserProfileWithDataFiles(CancellationToken cancellationToken, IRequestHeaders requestHeaders,
            IReadOnlyDictionary<string, string> fields)
        {
            var request = RequestsFactory.MultipartRestRequest(requestHeaders, Method.PUT, ApiCategories.Profile);
            FillRequestFilesWithNameValueCollection(request, fields);
            return SendRestRequest(request, cancellationToken);
        }

        public static Task<IRestResponse> UpdateUserProfileData(CancellationToken cancellationToken, IRequestHeaders requestHeaders,
            IReadOnlyDictionary<string, string> fields, string newAvatarImagePath)
        {
            var request = RequestsFactory.MultipartRestRequest(requestHeaders, Method.PUT, ApiCategories.Profile);
            if (!string.IsNullOrEmpty(newAvatarImagePath))
                request.AddFile(MainNames.ModelsPropertiesNames.Avatar, newAvatarImagePath);
            FillRequestParametersWithNameValueCollection(request, fields);

            return SendRestRequest(request, cancellationToken);
        }


        public static Task<BaseRequestProcessor<IUserProfilePasswordChangeModel, UserProfileResponseModel, IUserProfileResponseModel>.HttpResponse>
            TryChangeProfilePassword(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                IUserProfilePasswordChangeModel requestBodyModel)
        {
            return new UserProfilePasswordChangePutProcessor(out cancellationTokenSource, requestHeaders, requestBodyModel)
                .SendRequest("Profile password was changed successfully");
        }

        public static Task<BaseRequestProcessor<IUserGeoLocation, UserProfileDataModel, IUserProfileModel>.HttpResponse>
            UpdateUserPosition(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                IUserGeoLocation userGeoLocation)
        {
            return new UserGeoLocationDataPutProcessor(out cancellationTokenSource, requestHeaders, userGeoLocation)
                .SendRequest("User geo location was successfully sent to server");
        }

        public static Task<BaseRequestProcessor<object, MarketDiagramResponseDateModel, IMarketDiagramResponseModel>.HttpResponse>
            GetMarketDiagramDataAsync(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            return new MarketDiagramDataGetProcessor(out cancellationTokenSource, requestHeaders)
                .SendRequest("Market diagram data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, VerificationResponseDataModel, IVerificationResponseModel>.HttpResponse>
            GetVerificationData(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            return new VerificationDataGetProcessor(out cancellationTokenSource, requestHeaders)
                .SendRequest("Verification data vas retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, MerchantProfileResponseModel, IMerchantProfileResponseModel>.HttpResponse>
            GetMerchantProfileData(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            return new MerchantProfileDataGetProcessor(out cancellationTokenSource, requestHeaders)
                .SendRequest("Merchant profile data was retrieved successfully");
        }

        private static void FillRequestParametersWithNameValueCollection(IRestRequest request, IReadOnlyDictionary<string, string> collection)
        {
            foreach (var field in collection)
            {
                request.AddParameter(field.Key, field.Value);
            }
        }

        private static void FillRequestFilesWithNameValueCollection(IRestRequest request, IReadOnlyDictionary<string, string> collection)
        {
            foreach (var field in collection)
            {
                request.AddFile(field.Key, field.Value);
            }
        }

        private static Task<IRestResponse> SendRestRequest(IRestRequest request, CancellationToken cancellationToken)
        {
            LogUtility.PrintLog(Tag, request.ToString());
            return ApiHelper.DefaultRestClient.ExecuteAsync(request, cancellationToken);
        }

        private static void FillRequestParametersWithNameValueCollection(IRestRequest request, NameValueCollection collection, string containerName)
        {
            foreach (string fieldName in collection)
            {
                request.AddParameter($"{containerName}[{fieldName}]", collection[fieldName]);
            }
        }
    }
}