using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class GetInterestsQuestionsAnswersGetRequestProcessor : RequestWithoutBodyProcessor<InterestAnswersRequestDataModel,
        IInterestAnswersRequestModel>
    {
        public GetInterestsQuestionsAnswersGetRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource,
            IRequestHeaders requestHeaders, int interestId) : base(out cancellationTokenSource,
            ApiCategories.Subcategories.Interests, HttpMethod.Get, requestHeaders, new[] {interestId.ToString()})
        {
        }
    }
}