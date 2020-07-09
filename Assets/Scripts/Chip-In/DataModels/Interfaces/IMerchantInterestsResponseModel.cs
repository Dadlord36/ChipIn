using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IMerchantInterestsResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("communities")] MarketInterestDetailsDataModel[] LabelDetailsDataModel { get; set; }
    }
}