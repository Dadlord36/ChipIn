using System.Net.Http;
using System.Threading;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class CommunityClientsInterestsPaginatedGetProcessor : RequestWithoutBodyProcessor<CommunityInterestsResponseDataModel,
        ICommunityInterestsResponseModel>
    {
        public CommunityClientsInterestsPaginatedGetProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int communityId, PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Get,
            requestHeaders, new[] {communityId.ToString(), ApiCategories.Subcategories.Interests},
            paginatedRequestData.ConvertPaginationToNameValueCollection())
        {
        }
    }
}