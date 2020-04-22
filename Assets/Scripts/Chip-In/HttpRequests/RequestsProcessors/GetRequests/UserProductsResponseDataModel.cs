using DataModels;
using DataModels.Common;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProductsResponseDataModel : IUserProductsResponseModel
    {
        public bool Success { get; set; }
        public PaginationData Pagination { get; set; }
        public ProductDataModel[] ProductsData { get; set; }
    }
}