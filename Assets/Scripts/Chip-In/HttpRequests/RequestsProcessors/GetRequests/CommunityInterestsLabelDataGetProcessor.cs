using System.Net.Http;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;
using Repositories.Remote;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunityInterestsLabelDataGetProcessor : BaseRequestProcessor<object, CommunityInterestLabelDataRequestResponse,
        ICommunityInterestLabelDataRequestResponse>
    {
        public CommunityInterestsLabelDataGetProcessor(IRequestHeaders requestHeaders) : base(
            RequestsSuffixes.Communities, HttpMethod.Get,
            requestHeaders, null)
        {
        }
    }
}