using System.Threading.Tasks;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class AdvertStaticRequestsProcessor
    {
        public static Task<BaseRequestProcessor<object, AdvertsListResponseDataModel, IAdvertsListResponseModel>.HttpResponse>
            ListAdverts(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, PaginatedRequestData 
            paginatedRequestData)
        {
            return new AdvertsListGetRequestProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData).SendRequest(
                "Adverts list was retrieved successfully");
        }
    }
}