using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class ShowInterestSurveyGetProcessor : RequestWithoutBodyProcessor<SurveyRequestResponseDataModel, ISurveyRequestResponseModel>
    {
        public ShowInterestSurveyGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int interestId) : base(out cancellationTokenSource, ApiCategories.Subcategories.Interests, HttpMethod.Get, requestHeaders,
                new[] {interestId.ToString(), MainNames.ModelsPropertiesNames.Survey})
        {
        }
    }
}