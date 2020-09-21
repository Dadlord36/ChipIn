using DataModels.SimpleTypes;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IFlashOfferCreationRequestModel
    {
        [JsonProperty("flash_offer")] IFlashOfferGetRequestModel FlashOffer { get; set; }
        FilePath PosterFilePath { get; set; }
    }
}