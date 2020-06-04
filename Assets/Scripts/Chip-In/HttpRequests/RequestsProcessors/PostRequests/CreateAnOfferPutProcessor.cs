using System.Net.Http;
using System.Threading;
using DataModels.HttpRequestsHeadersModels;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class CreateAnOfferPutProcessor : BaseRequestProcessor<IOfferCreationRequestModel, CreateAnOfferResponseModel,
        ICreateAnOfferResponseModel>
    {
        public CreateAnOfferPutProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            IOfferCreationRequestModel requestBodyModel) : base(out cancellationTokenSource, ApiCategories.Offers, HttpMethod.Post,
            requestHeaders, requestBodyModel)
        {
        }
    }
}