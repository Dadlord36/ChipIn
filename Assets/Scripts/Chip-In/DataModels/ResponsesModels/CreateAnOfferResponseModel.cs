using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface ICreateAnOfferResponseModel : ISuccess
    {
        ChallengingOfferWithIdentifierModel Offer { get; set; }
    }

    public class CreateAnOfferResponseModel : ICreateAnOfferResponseModel
    {
        public bool Success { get; set; }
        public ChallengingOfferWithIdentifierModel Offer { get; set; }
    }
}