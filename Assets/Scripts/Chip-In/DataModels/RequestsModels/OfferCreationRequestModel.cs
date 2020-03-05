using Newtonsoft.Json;
using UnityEngine;

namespace DataModels.RequestsModels
{
    public interface IOfferCreationRequestModel 
    {
        [JsonProperty("offer")] UserCreatedOffer Offer { get; set; }
        [JsonProperty("poster")] TextAsset PosterAsText { get; set; }
    }

    public class OfferCreationRequestModel : IOfferCreationRequestModel
    {
        public TextAsset PosterAsText { get; set; }
        public UserCreatedOffer Offer { get; set; }
    }
}