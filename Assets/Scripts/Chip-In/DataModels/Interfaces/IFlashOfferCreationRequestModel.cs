using DataModels.RequestsModels;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IFlashOfferCreationRequestModel : IPosterImageFile
    {
        [JsonProperty("flash_offer")] IFlashOfferGetRequestModel FlashOffer { get; set; }
    }
}