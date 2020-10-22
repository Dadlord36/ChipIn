using Newtonsoft.Json;

namespace DataModels.RequestsModels
{
    public interface IOfferCreationRequestModel 
    {
        [JsonProperty("offer")] ClientOfferDataModel Offer { get; set; }
        [JsonProperty("poster")] string PosterImageFilePath { get; set; }
    }

    public class OfferCreationRequestModel : IOfferCreationRequestModel
    {
        public ClientOfferDataModel Offer { get; set; }
        public string PosterImageFilePath { get; set; }
    }
}