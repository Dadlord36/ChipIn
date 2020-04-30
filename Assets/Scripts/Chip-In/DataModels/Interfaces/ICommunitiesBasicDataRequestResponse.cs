using DataModels.Common;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface ICommunitiesBasicDataRequestResponse : ISuccess
    {
        [JsonProperty("pagination")] PaginationData Pagination { get; set; }
        [JsonProperty("communities")] CommunityBasicDataModel[] Communities { get; set; }
    }
}