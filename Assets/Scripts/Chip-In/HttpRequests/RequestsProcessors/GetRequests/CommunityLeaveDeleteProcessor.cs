using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunityLeaveDeleteProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel,
        ISuccess>
    {
        public CommunityLeaveDeleteProcessor(IRequestHeaders requestHeaders, int communityId) :
            base(ApiCategories.Communities, HttpMethod.Post, requestHeaders,
                new[] {communityId.ToString(), MainNames.CommonActions.Leave})
        {
        }
    }
}