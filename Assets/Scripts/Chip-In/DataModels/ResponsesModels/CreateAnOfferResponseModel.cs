using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface ICreateAnOfferResponseModel : ISuccess
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Offer)]
        ClientOfferDataModel Offer { get; set; }
    }

    public class CreateAnOfferResponseModel : ICreateAnOfferResponseModel
    {
        public bool Success { get; set; }
        public ClientOfferDataModel Offer { get; set; }
    }
}