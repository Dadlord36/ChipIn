using System.Net.Http;
using System.Threading;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunityLeaveDeleteProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel,
        ISuccess>
    {
        public CommunityLeaveDeleteProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int communityId) : base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Post, requestHeaders,
            new[] {communityId.ToString(), MainNames.CommonActions.Leave})
        {
        }
    }
}