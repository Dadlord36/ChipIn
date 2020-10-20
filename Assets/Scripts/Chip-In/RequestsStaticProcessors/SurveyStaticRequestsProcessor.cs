using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using HttpRequests.RequestsProcessors.PutRequests;
using Repositories.Interfaces;

namespace RequestsStaticProcessors
{
    public static class SurveyStaticRequestsProcessor
    {
        public static Task<BaseRequestProcessor<object, SurveyRequestResponseDataModel, ISurveyRequestResponseModel>.HttpResponse>
            ShowInterestSurvey(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders authorizationHeaders, int interestId)
        {
            return new ShowInterestSurveyGetProcessor(out cancellationTokenSource, authorizationHeaders, interestId)
                .SendRequest("Interest survey was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<IAnswerToSurveyQuestionBodyModel, SuccessConfirmationModel, ISuccess>.HttpResponse>
            AnswerToSurveyQuestions(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders authorizationHeaders,
                int interestId, IAnswerToSurveyQuestionBodyModel bodyModel)
        {
            return new AnswerToSurveyQuestionPutProcessor(out cancellationTokenSource, authorizationHeaders, interestId, bodyModel)
                .SendRequest("Answers to survey was sent successfully");
        }
    }
}