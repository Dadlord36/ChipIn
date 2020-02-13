using DataModels.Common;
using DataModels.Interfaces;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface IOffersResponseModel : ISuccess, IPagination
    {
        [JsonProperty("offers")] ChallengingOfferWithIdentifierModel[] Offers { get; set; }
    }

    public class OffersResponseModel : IOffersResponseModel
    {
        public bool Success { get; set; }
        public PaginationData Pagination { get; set; }
        public ChallengingOfferWithIdentifierModel[] Offers { get; set; }
    }


}