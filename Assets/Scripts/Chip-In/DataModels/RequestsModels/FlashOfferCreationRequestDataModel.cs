using DataModels.Interfaces;
using DataModels.SimpleTypes;

namespace DataModels.RequestsModels
{
    public class FlashOfferCreationRequestDataModel : IFlashOfferCreationRequestModel
    {
        public IFlashOfferGetRequestModel FlashOffer { get; set; }

        public FilePath PosterFilePath { get; set; } = new FilePath(string.Empty);
    }
}