using System.Net.Http;
using System.Threading;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class JoinInInterestPostRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public JoinInInterestPostRequestProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int interestId)
            : base(out cancellationTokenSource, ApiCategories.Subcategories.Interests, HttpMethod.Post, requestHeaders,
                new[] {interestId.ToString(), MainNames.CommonActions.Join})
        {
        }
    }
}