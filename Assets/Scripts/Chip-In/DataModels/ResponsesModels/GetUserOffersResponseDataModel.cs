using DataModels.Common;
using DataModels.Interfaces;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface IGetClientOffersResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("offers")] ClientOfferDataModel[] Offers { get; set; }
    }
    
    public class GetClientOffersResponseDataModel : IGetClientOffersResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public ClientOfferDataModel[] Offers { get; set; }
    }
}