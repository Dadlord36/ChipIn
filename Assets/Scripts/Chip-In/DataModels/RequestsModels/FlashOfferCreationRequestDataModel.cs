using DataModels.Interfaces;
using DataModels.SimpleTypes;

namespace DataModels.RequestsModels
{
    public class FlashOfferCreationRequestDataModel : IFlashOfferCreationRequestModel
    {
        public FilePath PosterFilePath { get; set; }

        public IFlashOfferGetRequestModel FlashOffer { get; set; }
    }
}