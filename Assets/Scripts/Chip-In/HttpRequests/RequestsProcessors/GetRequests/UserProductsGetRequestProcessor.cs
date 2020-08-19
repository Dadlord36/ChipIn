using System.Net.Http;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProductsGetRequestProcessor : RequestWithoutBodyProcessor<UserProductsResponseDataModel, IUserProductsResponseModel>
    {
        public UserProductsGetRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, PaginatedRequestData
            paginatedRequestData) : base(out cancellationTokenSource, ApiCategories.UserProducts, HttpMethod.Get, requestHeaders, paginatedRequestData)
        {
        }
    }
}