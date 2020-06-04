using System.Net.Http;
using System.Threading;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class CommunityOwnersInterestsPaginatedGetProcessor : RequestWithoutBodyProcessor<CommunityInterestsResponseDataModel,
        ICommunityInterestsResponseModel>
    {
        public CommunityOwnersInterestsPaginatedGetProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Get, requestHeaders,
            new[] {ApiCategories.Subcategories.Interests}, paginatedRequestData.ConvertPaginationToNameValueCollection())
        {
        }
    }
}