using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.RequestsModels
{
    public class MerchantInterestsResponseDataModel : IMerchantInterestsResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public MarketInterestDetailsDataModel[] LabelDetailsDataModel { get; set; }
    }
}