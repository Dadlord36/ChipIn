using System.Net.Http;
using System.Threading;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunityJoinPostProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel,
        ISuccess>
    {
        public CommunityJoinPostProcessor(out CancellationTokenSource cancellationTokenSource,
            IRequestHeaders requestHeaders, int communityId) :
            base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Post, requestHeaders,
                new[] {communityId.ToString(), MainNames.CommunityActions.Join})
        {
        }
    }
}