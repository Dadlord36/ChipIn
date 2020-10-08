using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.RequestsModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class CreateCommunityInterestPostProcessor : BaseRequestProcessor<ICommunityCreateInterestRequestBodyModel, SuccessConfirmationModel, ISuccess>
    {
        public CreateCommunityInterestPostProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            ICommunityCreateInterestRequestBodyModel requestBodyModel) : base(out cancellationTokenSource,
            new BaseRequestProcessorParameters(ApiCategories.Communities, HttpMethod.Post, requestHeaders, requestBodyModel,
                new[] {ApiCategories.Subcategories.Interests}))
        {
        }
    }
}