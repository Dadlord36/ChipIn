using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface ICommunityInterestsResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("interests")] CommunityInterestDataModel[] Interests { get; set; }
    }
}