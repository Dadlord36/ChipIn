using System.Net.Http;
using DataModels.HttpRequestsHeadersModels;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class CreateAnOfferPutProcessor : BaseRequestProcessor<IOfferCreationRequestModel, CreateAnOfferResponseModel
        , ICreateAnOfferResponseModel>
    {
        public CreateAnOfferPutProcessor(IRequestHeaders requestHeaders, IOfferCreationRequestModel requestBodyModel) :
            base(RequestsSuffixes.Offers, HttpMethod.Post, requestHeaders, requestBodyModel)
        {
        }
    }
}