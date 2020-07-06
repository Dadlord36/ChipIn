using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IMarketInterestsDetailsDataRequestResponse : ISuccess, IPaginatedResponse
    {
        [JsonProperty("communities")] MarketInterestDetailsDataModel[] MarketInterestsDetails { get; set; }
    }
}