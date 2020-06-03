using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunityJoinPostProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel,
        ISuccess>
    {
        public CommunityJoinPostProcessor(IRequestHeaders requestHeaders, int communityId) :
            base(ApiCategories.Communities, HttpMethod.Post, requestHeaders,
                new[] {communityId.ToString(), MainNames.CommonActions.Join})
        {
        }
    }
}