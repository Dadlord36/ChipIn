using System;
using System.Threading.Tasks;
using DataModels;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors.GetRequests;
using HttpRequests.RequestsProcessors.PostRequests;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class OffersStaticRequestProcessor
    {
        public static async Task<PaginatedList<ChallengingOfferWithIdentifierModel>> GetListOfOffers(
            IRequestHeaders requestHeaders)
        {
            var response =
                await new OffersGetProcessor(requestHeaders).SendRequest("Offers was retrieved successfully");

            PaginatedList<ChallengingOfferWithIdentifierModel> Create(IOffersResponseModel offersResponseModel)
            {
                return new PaginatedList<ChallengingOfferWithIdentifierModel>(offersResponseModel.Pagination,
                    offersResponseModel.Offers);
            }

            return Create(response.ResponseModelInterface);
        }

        public static async Task<IOfferDetailsResponseModel> GetOfferDetails(
            DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters parameters)
        {
            var response =
                await new DetailedOfferGetProcessor(parameters).SendRequest("Offer details was retrieved successfully");
            return response.ResponseModelInterface;
        }

        public static async Task<ChallengingOfferWithIdentifierModel> CreateAnOffer(IRequestHeaders requestHeaders,
            IOfferCreationRequestModel requestModel)
        {
            try
            {
                var result =
                    await new CreateAnOfferPutProcessor(requestHeaders, requestModel).SendRequest(
                        "Offer was create successfully");
                var response = result.ResponseModelInterface;
                return response.Offer;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}