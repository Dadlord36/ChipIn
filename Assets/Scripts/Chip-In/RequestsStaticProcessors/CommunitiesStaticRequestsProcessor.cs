﻿using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Interfaces;

namespace RequestsStaticProcessors
{
    public static class CommunitiesStaticRequestsProcessor
    {
        public static Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            GetCommunitiesList(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            var processor = new CommunitiesListGetProcessor(out cancellationTokenSource, requestHeaders);
            return processor.SendRequest("Communities data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            GetPaginatedCommunitiesList(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData)
        {
            return new CommunitiesPaginatedListGetProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData)
                .SendRequest("Communities paginated data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunityItemResponseDataModel, ICommunityItemResponseModel>.HttpResponse>
            GetCommunityDetails(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int communityId)
        {
            return new CommunityDetailsGetProcessor(out cancellationTokenSource, requestHeaders, communityId).SendRequest(
                "Community details data was retrieved");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            JoinCommunity(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int communityId)
        {
            return new CommunityJoinPostProcessor(out cancellationTokenSource, requestHeaders, communityId)
                .SendRequest($"Joined successfully to community {communityId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            LeaveCommunity(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int communityId)
        {
            return new CommunityLeaveDeleteProcessor(out cancellationTokenSource, requestHeaders, communityId)
                .SendRequest($"Leaving successfully community {communityId.ToString()}");
        }
    }
}