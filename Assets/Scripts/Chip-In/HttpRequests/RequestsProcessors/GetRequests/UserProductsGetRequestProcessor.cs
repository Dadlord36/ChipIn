using System.Net.Http;
using System.Threading;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProductsGetRequestProcessor : BaseRequestProcessor<object, UserProductsResponseDataModel,
        IUserProductsResponseModel>
    {
        public UserProductsGetRequestProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders) :
            base(out cancellationTokenSource, ApiCategories.UserProducts, HttpMethod.Get, requestHeaders, null)
        {
        }
    }
}