using Newtonsoft.Json;

namespace DataModels.RequestsModels
{
    public interface IOfferCreationRequestModel 
    {
        [JsonProperty("offer")] UserCreatedOffer Offer { get; set; }
    }

    public class OfferCreationRequestModel : IOfferCreationRequestModel
    {
        public UserCreatedOffer Offer { get; set; }
    }
    
}