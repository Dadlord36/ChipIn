using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public sealed class FlashOfferCreationResponseDataModel : IFlashOfferCreationResponseModel
    {
        public bool Success { get; set; }
        public FlashOfferCreationResponseDataModel FlashModel { get; set; }
    }
}