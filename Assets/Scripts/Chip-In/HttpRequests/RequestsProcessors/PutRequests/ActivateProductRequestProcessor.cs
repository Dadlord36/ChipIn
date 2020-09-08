using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.PutRequests
{
    
    public class ProductQrCode : IQrData, ITotalBill
    {
        public string QrData { get; set; }
        public uint TotalBill { get; set; }


        public ProductQrCode(string qrData, uint totalBill)
        {
            QrData = qrData;
            TotalBill = totalBill;
        }
    }

    public class ActivateProductRequestProcessor : BaseRequestProcessor<IQrData, SuccessConfirmationModel,
        ISuccess>
    {
        public ActivateProductRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            IQrData requestBodyModel) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            ApiCategories.UserProducts, HttpMethod.Post, requestHeaders, requestBodyModel, new[] {"activate"}))
        {
        }
    }

    public class DeleteProductRequestProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public DeleteProductRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int productId) : base(out cancellationTokenSource, ApiCategories.UserProducts, HttpMethod.Delete, requestHeaders,
            new[] {productId.ToString()})
        {
        }
    }
}