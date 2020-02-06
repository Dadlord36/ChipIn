using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IOfferDetailsResponseModel : ISuccess
    {
        [JsonProperty("offer")] OfferDetailedModel Offer { get; set; }
    }
}