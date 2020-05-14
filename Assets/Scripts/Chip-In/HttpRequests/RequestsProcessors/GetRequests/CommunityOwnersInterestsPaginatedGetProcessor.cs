using System.Net.Http;
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
        public CommunityOwnersInterestsPaginatedGetProcessor(IRequestHeaders requestHeaders, PaginatedRequestData paginatedRequestData) :
            base(ApiCategories.Communities, HttpMethod.Get, requestHeaders,
                new[] {ApiCategories.Subcategories.Interests},
                paginatedRequestData.ConvertPaginationToNameValueCollection())
        {
        }
    }
}