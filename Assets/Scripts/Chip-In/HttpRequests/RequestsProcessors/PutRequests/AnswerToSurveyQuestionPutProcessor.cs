using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.PutRequests
{
    public class AnswerToSurveyQuestionPutProcessor : BaseRequestProcessor<IAnswerToSurveyQuestionBodyModel, SuccessConfirmationModel, ISuccess>
    {
        public AnswerToSurveyQuestionPutProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int interestId, IAnswerToSurveyQuestionBodyModel body) : base(out cancellationTokenSource, ApiCategories.Subcategories.Interests,
            HttpMethod.Post, requestHeaders, body, new[]
            {
                interestId.ToString(), ApiCategories.Subcategories.Survey,
                ApiCategories.Subcategories.Answer
            })
        {
        }
    }
}