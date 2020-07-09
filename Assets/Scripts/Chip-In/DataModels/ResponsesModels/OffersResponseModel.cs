using DataModels.Common;
using DataModels.Interfaces;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface IOffersResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("offers")] ChallengingOfferWithIdentifierModel[] Offers { get; set; }
    }

    public class OffersResponseModel : IOffersResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public ChallengingOfferWithIdentifierModel[] Offers { get; set; }
    }


}