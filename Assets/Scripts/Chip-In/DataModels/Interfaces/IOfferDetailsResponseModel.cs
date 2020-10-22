using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IOfferDetailsResponseModel : ISuccess
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Offer)]
        ClientOfferDataModel OfferData { get; set; }
    }
}