using System.Net;
using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserProductByQrGetRequestProcessor : RequestWithoutBodyProcessor<UserProductResponseDataModel, IUserProductResponseModel>
    {
        public UserProductByQrGetRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            in string qrCodeString) : base(out cancellationTokenSource, ApiCategories.UserProducts,
            HttpMethod.Get, requestHeaders, new[] {WebUtility.UrlEncode(qrCodeString)})
        {
        }
    }
}