using System.Collections.Specialized;
using System.Net.Http;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class OwnerOffersGetProcessor : RequestWithoutBodyProcessor<GetOwnerOffersResponseDataModel, IGetOwnerOffersResponseModel>
    {
        public OwnerOffersGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource, ApiCategories.Offers, HttpMethod.Get, requestHeaders,
            paginatedRequestData)
        {
        }
    }

    public sealed class ClientsOffersGetProcessor : RequestWithoutBodyProcessor<GetClientOffersResponseDataModel, IGetClientOffersResponseModel>
    {
        public ClientsOffersGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData, int interestId) : base(out cancellationTokenSource, ApiCategories.Offers, HttpMethod.Get,
            requestHeaders, paginatedRequestData, new NameValueCollection {{"interest_id", interestId.ToString()}})
        {
        }
    }

    public sealed class BuyPurchasePostProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public BuyPurchasePostProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int offerId) : base(out cancellationTokenSource, ApiCategories.Offers, HttpMethod.Post, requestHeaders,
            new[]
            {
                offerId.ToString(), "purchase"
            })
        {
        }
    }
}