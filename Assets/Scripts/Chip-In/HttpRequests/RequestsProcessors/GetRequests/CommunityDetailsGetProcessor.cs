using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public interface ICommunityItemResponseModel : ISuccess
    {
        [JsonProperty("community")] CommunityDetailsDataModel LabelDetailsDataModel { get; set; }
    }

    public sealed class CommunityDetailsGetProcessor : RequestWithoutBodyProcessor<CommunityItemResponseDataModel,
        ICommunityItemResponseModel>
    {
        public CommunityDetailsGetProcessor(IRequestHeaders requestHeaders, int communityId) :
            base(ApiCategories.Communities, HttpMethod.Get, requestHeaders, new []{communityId.ToString()})
        {
        }
    }
}