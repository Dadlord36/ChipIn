using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface ICommunitiesBasicDataRequestResponse : ISuccess, IPaginatedResponse
    {
        [JsonProperty("communities")] InterestBasicDataModel[] Communities { get; set; }
    }
}