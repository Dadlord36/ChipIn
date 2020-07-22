using System.Net.Http;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class AdvertsListGetRequestProcessor : RequestWithoutBodyProcessor<AdvertsListResponseDataModel, IAdvertsListResponseModel>
    {
        public AdvertsListGetRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, PaginatedRequestData
            paginatedRequestData) : base(out cancellationTokenSource, ApiCategories.Adverts, HttpMethod.Get, requestHeaders, paginatedRequestData)
        {
        }
    }
}