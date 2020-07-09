using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class CreateACommunityInterestPostProcessor : BaseRequestProcessor<ICommunityCreateInterestModel,
        MerchantInterestPageDataModel, IInterestPageModel>
    {
        public CreateACommunityInterestPostProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            ICommunityCreateInterestModel requestBodyModel, int communityId) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            ApiCategories.Communities, HttpMethod.Post, requestHeaders, requestBodyModel,
            new[] {communityId.ToString(), ApiCategories.Subcategories.Interests}))
        {
        }
    }
}