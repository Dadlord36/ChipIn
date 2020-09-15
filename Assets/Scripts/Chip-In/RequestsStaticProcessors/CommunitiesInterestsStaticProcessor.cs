using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.DeleteRequests;
using HttpRequests.RequestsProcessors.GetRequests;
using HttpRequests.RequestsProcessors.PostRequests;
using Repositories.Interfaces;

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
            GetClientsInterestPages(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                int communityId, string categoryName, PaginatedRequestData paginatedRequestData)
        {
            return new CommunityClientsInterestsPaginatedGetProcessor(out cancellationTokenSource, requestHeaders, communityId, categoryName,
                paginatedRequestData).SendRequest("Community clients interests list was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<ICommunityCreateInterestModel, MerchantInterestPageDataModel, IInterestPageModel>.HttpResponse>
            CreateAnInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                ICommunityCreateInterestModel requestBody, int communityId)
        {
            return new CreateACommunityInterestPostProcessor(out cancellationTokenSource, requestHeaders, requestBody, communityId).SendRequest(
                "Community interest was created successfully");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            DeleteCommunityInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int communityId,
                int interestId)
        {
            return new DestroyCommunityInterestDeleteProcessor(out cancellationTokenSource, requestHeaders, communityId, interestId).SendRequest(
                $"Community {communityId.ToString()} interest {interestId.ToString()} was removed successfully");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            JoinToInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
        {
            return new JoinInInterestPostRequestProcessor(out cancellationTokenSource, requestHeaders, interestId).SendRequest(
                $"Successfully joining the interest by index: {interestId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            SupportInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
        {
            return new SupportInterestPostRequestProcessor(out cancellationTokenSource, requestHeaders, interestId).SendRequest(
                $"Successfully supported the interest by index: {interestId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse> 
            FundInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId, int tokensAmount)
        {
            return new FundInterestPostRequestProcessor(out cancellationTokenSource, requestHeaders, interestId, tokensAmount).SendRequest(
                $"Successfully fund the interest by index: {interestId.ToString()}");
        }


        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            LeaveAnInterest(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
        {
            return new LeaveAnInterestDeleteRequestProcessor(out cancellationTokenSource, requestHeaders, interestId).SendRequest(
                $"Successfully left the interest by index: {interestId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, InterestAnswersRequestDataModel, IInterestAnswersRequestModel>.HttpResponse>
            GetInterestQuestionsAnswers(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
        {
            return new GetInterestsQuestionsAnswersGetRequestProcessor(out cancellationTokenSource, requestHeaders, interestId)
                .SendRequest("Interest survey was retrieved successfully");
        }
    }
}