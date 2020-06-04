using System.Net.Http;
using System.Threading;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class CreateACommunityInterestPostProcessor : BaseRequestProcessor<ICommunityCreateInterestModel,
        CommunityInterestDataModel, ICommunityInterestModel>
    {
        public CreateACommunityInterestPostProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            ICommunityCreateInterestModel requestBodyModel, int communityId) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            ApiCategories.Communities, HttpMethod.Post, requestHeaders, requestBodyModel,
            new[] {communityId.ToString(), ApiCategories.Subcategories.Interests}))
        {
        }
    }
}