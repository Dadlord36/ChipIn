using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public sealed class MerchantInterestPagesResponseDataModel : IMerchantInterestPagesResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public MerchantInterestPageDataModel[] Interests { get; set; }
    }
}