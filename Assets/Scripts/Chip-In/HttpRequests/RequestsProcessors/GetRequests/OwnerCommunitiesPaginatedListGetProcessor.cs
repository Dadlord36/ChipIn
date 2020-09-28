using System.Net.Http;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class OwnerCommunitiesPaginatedListGetProcessor : RequestWithoutBodyProcessor<MerchantInterestsResponseDataModel,
        IMerchantInterestsResponseModel>
    {
        public OwnerCommunitiesPaginatedListGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, 
            IRequestHeaders requestHeaders,PaginatedRequestData paginatedRequestData) 
            : base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Get, requestHeaders, paginatedRequestData)
        {
        }
    }
}