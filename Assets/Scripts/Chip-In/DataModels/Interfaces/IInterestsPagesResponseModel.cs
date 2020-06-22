using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IInterestsPagesResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("interests")] InterestPagePageDataModel[] Interests { get; set; }
    }
}