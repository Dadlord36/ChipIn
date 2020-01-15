using DataModels.ResponsesModels;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface ICommunityInterestLabelDataRequestResponse : ISuccess
    {
        [JsonProperty("pagination")]  CommunityInterestLabelDataRequestResponse.PaginationData Pagination { get; set; }
        [JsonProperty("communities")] CommunityInterestLabelDataRequestResponse.CommunityInterestLabelData[] Communities { get; set; }
    }
}