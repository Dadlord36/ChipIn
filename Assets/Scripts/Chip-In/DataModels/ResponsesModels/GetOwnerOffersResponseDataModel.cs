using DataModels.Common;
using DataModels.Interfaces;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface IGetOwnerOffersResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("offers")] OwnerOfferDataModel[] Offers { get; set; }
    }

    public class GetOwnerOffersResponseDataModel : IGetOwnerOffersResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }

        public OwnerOfferDataModel[] Offers { get; set; }
    }
}