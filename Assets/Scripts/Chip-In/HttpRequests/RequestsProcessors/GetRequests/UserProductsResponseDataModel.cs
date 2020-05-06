using DataModels;
using DataModels.Common;
using DataModels.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProductsResponseDataModel : IUserProductsResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public ProductDataModel[] ProductsData { get; set; }
    }
}