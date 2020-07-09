using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProductsGetRequestProcessor : BaseRequestProcessor<object, UserProductsResponseDataModel,
        IUserProductsResponseModel>
    {
        public UserProductsGetRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders) :
            base(out cancellationTokenSource, ApiCategories.UserProducts, HttpMethod.Get, requestHeaders, null)
        {
        }
    }
}