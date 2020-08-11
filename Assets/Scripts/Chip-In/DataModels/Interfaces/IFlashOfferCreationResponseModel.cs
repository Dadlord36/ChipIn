using DataModels.ResponsesModels;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IFlashOfferCreationResponseModel : ISuccess
    {
        [JsonProperty("flash_offer")] FlashOfferCreationResponseDataModel FlashModel { get; set; }
    }
}