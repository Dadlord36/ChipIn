using System.Threading.Tasks;
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
            GetUserProducts(IRequestHeaders requestHeaders)
        {
            return new UserProductsGetRequestProcessor(requestHeaders).SendRequest(
                "User products was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<IQrData, SuccessConfirmationModel, ISuccess>.HttpResponse>
            ActivateProduct(IRequestHeaders requestHeaders, IQrData requestBodyModel)
        {
            return new ActivateProductRequestProcessor(requestHeaders, requestBodyModel).SendRequest(
                $"Product {requestBodyModel.QrData} was activated");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            DeleteUserProduct(IRequestHeaders requestHeaders, int productId)
        {
            return new DeleteProductRequestProcessor(requestHeaders, productId).SendRequest(
                $"Product with id: {productId.ToString()} was successfully deleted");
        }
    }
}