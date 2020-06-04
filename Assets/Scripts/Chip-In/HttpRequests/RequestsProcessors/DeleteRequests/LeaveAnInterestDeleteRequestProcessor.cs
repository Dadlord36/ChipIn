using System.Net.Http;
using System.Threading;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.DeleteRequests
{
    public class LeaveAnInterestDeleteRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public LeaveAnInterestDeleteRequestProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int interestId) : base(out cancellationTokenSource, ApiCategories.Subcategories.Interests, HttpMethod.Delete, requestHeaders,
            new[] {interestId.ToString(), MainNames.CommonActions.Leave})
        {
        }
    }
}