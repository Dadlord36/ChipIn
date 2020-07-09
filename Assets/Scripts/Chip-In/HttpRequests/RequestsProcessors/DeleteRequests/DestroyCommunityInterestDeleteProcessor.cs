using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.DeleteRequests
{
    public class DestroyCommunityInterestDeleteProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public DestroyCommunityInterestDeleteProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int communityId, int interestId) : base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Delete, requestHeaders,
            new[] {communityId.ToString(), ApiCategories.Subcategories.Interests, interestId.ToString()})
        {
        }
    }
}