using System.Threading.Tasks;
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
        public static Task<BaseRequestProcessor<object, CommunityInterestsResponseDataModel,
                ICommunityInterestsResponseModel>.HttpResponse>
            GetCommunityOwnersInterests(IRequestHeaders requestHeaders, PaginatedRequestData paginatedRequestData)
        {
            return new CommunityOwnersInterestsPaginatedGetProcessor(requestHeaders, paginatedRequestData)
                .SendRequest("Community owners interests list was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunityInterestsResponseDataModel,
            ICommunityInterestsResponseModel>.HttpResponse> GetCommunityClientsInterests(IRequestHeaders requestHeaders,
            int communityId, PaginatedRequestData paginatedRequestData)
        {
            return new CommunityClientsInterestsPaginatedGetProcessor(requestHeaders, communityId, paginatedRequestData)
                .SendRequest("Community clients interests list was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<ICommunityCreateInterestModel, CommunityInterestDataModel,
            ICommunityInterestModel>.HttpResponse> CreateAnInterest(IRequestHeaders requestHeaders,
            ICommunityCreateInterestModel requestBody, int communityId)
        {
            return new CreateACommunityInterestPostProcessor(requestHeaders, requestBody, communityId).SendRequest(
                "Community interest was created successfully");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            DeleteCommunityInterest(IRequestHeaders requestHeaders, int communityId, int interestId)
        {
            return new DestroyCommunityInterestDeleteProcessor(requestHeaders, communityId, interestId).SendRequest(
                $"Community {communityId.ToString()} interest {interestId.ToString()} was removed successfully");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            JoinToInterest(IRequestHeaders requestHeaders, int interestId)
        {
            return new JoinInInterestPostRequestProcessor(requestHeaders, interestId).SendRequest(
                $"Successfully joining the interest by index: {interestId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            LeaveAnInterest(IRequestHeaders requestHeaders, int interestId)
        {
            return new LeaveAnInterestDeleteRequestProcessor(requestHeaders, interestId).SendRequest(
                $"Successfully leaved the interest by index: {interestId.ToString()}");
        }
    }
}