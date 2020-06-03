using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.DeleteRequests
{
    public class DestroyCommunityInterestDeleteProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public DestroyCommunityInterestDeleteProcessor(IRequestHeaders requestHeaders, int communityId, int interestId)
            : base(ApiCategories.Communities, HttpMethod.Delete, requestHeaders,
                new[] {communityId.ToString(), ApiCategories.Subcategories.Interests, interestId.ToString()})
        {
        }
    }
}