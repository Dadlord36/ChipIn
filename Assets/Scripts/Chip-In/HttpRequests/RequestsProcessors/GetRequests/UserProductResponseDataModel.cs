using DataModels;
using DataModels.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProductResponseDataModel : IUserProductResponseModel
    {
        public bool Success { get; set; }
        public ProductDataModel ProductsData { get; set; }
    }
}