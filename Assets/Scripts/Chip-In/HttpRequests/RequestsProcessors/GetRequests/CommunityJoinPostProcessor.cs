using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunityJoinPostProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel,
        ISuccess>
    {
        public CommunityJoinPostProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int communityId) : base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Post, requestHeaders,
            new[] {communityId.ToString(), MainNames.CommonActions.Join})
        {
        }
    }
}