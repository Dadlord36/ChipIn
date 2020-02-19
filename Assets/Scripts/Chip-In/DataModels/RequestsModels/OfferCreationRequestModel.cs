using DataModels.Interfaces;
using DataModels.SimpleTypes;
using Newtonsoft.Json;

namespace DataModels.RequestsModels
{
    public interface IOfferCreationRequestModel : IPosterImageFile
    {
        [JsonProperty("offer")] UserCreatedOffer Offer { get; set; }
    }

    public class OfferCreationRequestModel : IOfferCreationRequestModel
    {
        [JsonProperty("poster")] public FilePath PosterFilePath { get; set; }
        [JsonProperty("offer")] public UserCreatedOffer Offer { get; set; }
    }
}