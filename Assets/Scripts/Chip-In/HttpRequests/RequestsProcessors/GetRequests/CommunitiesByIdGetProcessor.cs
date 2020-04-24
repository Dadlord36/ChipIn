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
        [JsonProperty("community")] CommunityInterestLabelData LabelData { get; set; }
    }

    public sealed class CommunitiesByIdGetProcessor : RequestWithoutBodyProcessor<CommunityItemResponseDataModel,
        ICommunityItemResponseModel>
    {
        public CommunitiesByIdGetProcessor(IRequestHeaders requestHeaders, int communityId) :
            base(ApiCategories.Communities, HttpMethod.Get, requestHeaders, new []{communityId.ToString()})
        {
        }
    }
}