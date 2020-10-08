using System.Threading;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using HttpRequests;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.DeleteRequests;
using HttpRequests.RequestsProcessors.GetRequests;
using HttpRequests.RequestsProcessors.PostRequests;
using Repositories.Interfaces;
using RestSharp;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class CommunitiesInterestsStaticProcessor
    {
        public static Task<BaseRequestProcessor<object, MerchantInterestPagesResponseDataModel, IMerchantInterestPagesResponseModel>.HttpResponse>
            GetMerchantInterestPages(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                int selectedCommunityId, PaginatedRequestData paginatedRequestData)
        {
            return new MerchantInterestsPagesPaginatedGetProcessor(out cancellationTokenSource, requestHeaders, selectedCommunityId, paginatedRequestData)
                .SendRequest("Community owners interests list was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, UserInterestPagesResponseDataModel, IUserInterestPagesResponseModel>.HttpResponse>
            GetAllClientsInterestPages(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int communityId,
                string categoryName, PaginatedRequestData paginatedRequestData)
        {
            return new CommunityClientsInterestsPaginatedGetProcessor(out cancellationTokenSource, requestHeaders, communityId, categoryName,
                paginatedRequestData).SendRequest("Community clients interests list was retrieved successfully");
        }

        public static Task<IRestResponse> CreateAnInterestAsync(CancellationToken cancellationToken, IRequestHeaders requestHeaders, int communityId,
            InterestCreationDataModel requestBody)
        {
            var requestBuilder = new MultipartRestRequestBuilder(requestHeaders, Method.POST,
                $"{ApiCategories.Communities}/{communityId.ToString()}/{ApiCategories.Subcategories.Interests}",
                ApiHelper.ExecuteRequestWithDefaultRestClient, "interest");

            requestBuilder.AddItemParam(MainNames.ModelsPropertiesNames.IsPublic, PropertiesUtility.BoolToString(requestBody.IsPublic));
            requestBuilder.AddItemParam(MainNames.ModelsPropertiesNames.Name, requestBody.Name);
            requestBuilder.AddItemParam(MainNames.ModelsPropertiesNames.Segment, requestBody.SegmentName);
            requestBuilder.AddItemParam(MainNames.ModelsPropertiesNames.MemberMessage, requestBody.MemberMessage);
            requestBuilder.AddItemParam(MainNames.ModelsPropertiesNames.MerchantMessage, requestBody.MerchantMessage);

            requestBuilder.AddFileParam(MainNames.ModelsPropertiesNames.Poster, requestBody.PosterFilePath.Path);

            {
                requestBuilder.InitializeArrayParameter("user_interests_attributes");
                var userAttributes = requestBody.UserAttributes;
                for (var index = 0; index < userAttributes.Count; index++)
                {
                    requestBuilder.AddArrayParameterItemParameter(MainNames.ModelsPropertiesNames.UserId, index, userAttributes[index].UserId.ToString());
                }
            }

            return requestBuilder.ExecuteAsync(cancellationToken);
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            DeleteCommunityInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int communityId,
                int interestId)
        {
            return new DestroyCommunityInterestDeleteProcessor(out cancellationTokenSource, requestHeaders, communityId, interestId)
                .SendRequest($"Community {communityId.ToString()} interest {interestId.ToString()} was removed successfully");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            JoinToInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
        {
            return new JoinInInterestPostRequestProcessor(out cancellationTokenSource, requestHeaders, interestId)
                .SendRequest($"Successfully joining the interest by index: {interestId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            SupportInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
        {
            return new SupportInterestPostRequestProcessor(out cancellationTokenSource, requestHeaders, interestId)
                .SendRequest($"Successfully supported the interest by index: {interestId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            FundInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId, int tokensAmount)
        {
            return new FundInterestPostRequestProcessor(out cancellationTokenSource, requestHeaders, interestId, tokensAmount)
                .SendRequest($"Successfully fund the interest by index: {interestId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            LeaveAnInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
        {
            return new LeaveAnInterestDeleteRequestProcessor(out cancellationTokenSource, requestHeaders, interestId)
                .SendRequest($"Successfully left the interest by index: {interestId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, InterestAnswersRequestDataModel, IInterestAnswersRequestModel>.HttpResponse>
            GetInterestQuestionsAnswers(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
        {
            return new GetInterestsQuestionsAnswersGetRequestProcessor(out cancellationTokenSource, requestHeaders, interestId)
                .SendRequest("Interest survey was retrieved successfully");
        }
    }
}