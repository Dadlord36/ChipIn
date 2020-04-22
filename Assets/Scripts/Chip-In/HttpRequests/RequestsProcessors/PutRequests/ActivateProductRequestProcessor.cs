using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.PutRequests
{
    public class ProductQrCode : IQrData
    {
        public string QrData { get; set; }
        public ProductQrCode(string qrData)
        {
            QrData = qrData;
        }

    }

    public class ActivateProductRequestProcessor : BaseRequestProcessor<IQrData, SuccessConfirmationModel,
        ISuccess>
    {
        public ActivateProductRequestProcessor(IRequestHeaders requestHeaders, IQrData requestBodyModel) :
            base(new BaseRequestProcessorParameters(ApiCategories.UserProducts, HttpMethod.Post, requestHeaders,
                requestBodyModel, new[] {"activate"}))
        {
        }
    }

    public class DeleteProductRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public DeleteProductRequestProcessor(IRequestHeaders requestHeaders, int productId) :
            base(ApiCategories.UserProducts, HttpMethod.Delete,
                requestHeaders, new[] {productId.ToString()})
        {
        }
    }
}