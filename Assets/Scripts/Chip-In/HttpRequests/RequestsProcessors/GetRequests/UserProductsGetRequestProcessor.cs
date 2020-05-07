using System.Net.Http;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProductsGetRequestProcessor : BaseRequestProcessor<object, UserProductsResponseDataModel,
        IUserProductsResponseModel>
    {
        public UserProductsGetRequestProcessor(IRequestHeaders requestHeaders) : 
            base(ApiCategories.UserProducts, HttpMethod.Get, requestHeaders, null)
        {
        }
    }
}