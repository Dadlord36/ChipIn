using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IUserInterestsPagesResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("interests")] UserInterestPageDataModel[] Interests { get; set; }
    }
}