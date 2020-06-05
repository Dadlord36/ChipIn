using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using HttpRequests.RequestsProcessors.PutRequests;
using Repositories.Interfaces;

namespace RequestsStaticProcessors
{
    public static class UserProductsStaticRequestsProcessor
    {
        public static Task<BaseRequestProcessor<object, UserProductsResponseDataModel, IUserProductsResponseModel>.HttpResponse>
            GetUserProducts(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            return new UserProductsGetRequestProcessor(out cancellationTokenSource, requestHeaders).SendRequest(
                "User products was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<IQrData, SuccessConfirmationModel, ISuccess>.HttpResponse> ActivateProduct(
            out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, IQrData requestBodyModel)
        {
            return new ActivateProductRequestProcessor(out cancellationTokenSource, requestHeaders, requestBodyModel).SendRequest(
                $"Product {requestBodyModel.QrData} was activated");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            DeleteUserProduct(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int productId)
        {
            return new DeleteProductRequestProcessor(out cancellationTokenSource, requestHeaders, productId).SendRequest(
                $"Product with id: {productId.ToString()} was successfully deleted");
        }
    }
}