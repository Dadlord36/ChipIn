using System.Net.Http;
using System.Threading;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;
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
        public ActivateProductRequestProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            IQrData requestBodyModel) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            ApiCategories.UserProducts, HttpMethod.Post, requestHeaders, requestBodyModel, new[] {"activate"}))
        {
        }
    }

    public class DeleteProductRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public DeleteProductRequestProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int productId) : base(out cancellationTokenSource, ApiCategories.UserProducts, HttpMethod.Delete, requestHeaders,
            new[] {productId.ToString()})
        {
        }
    }
}