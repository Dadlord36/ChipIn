using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class OfferDetailsResponseModel : IOfferDetailsResponseModel
    {
        public bool Success { get; set; }
        public ClientOfferDataModel OfferData { get; set; }
    }
}