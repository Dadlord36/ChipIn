using Newtonsoft.Json;
using UnityEngine;

namespace DataModels.RequestsModels
{
    public interface IOfferCreationRequestModel 
    {
        [JsonProperty("offer")] UserCreatedOffer Offer { get; set; }
        [JsonProperty("poster")] string PosterImageFilePath { get; set; }
    }

    public class OfferCreationRequestModel : IOfferCreationRequestModel
    {
        public UserCreatedOffer Offer { get; set; }
        public string PosterImageFilePath { get; set; }
    }
}