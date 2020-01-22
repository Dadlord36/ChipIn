using System.Threading.Tasks;
using DataModels;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class OffersStaticRequestProcessor
    {
        public static async Task<PaginatedList<OfferBasicModel>> GetListOfOffers(IRequestHeaders requestHeaders)
        {
            var response =
                await new OffersGetProcessor(requestHeaders).SendRequest("Offers was retrieved successfully");

            PaginatedList<OfferBasicModel> Create(IOffersResponseModel offersResponseModel)
            {
                return new PaginatedList<OfferBasicModel>(offersResponseModel.Pagination, offersResponseModel.Offers);
            }

            return Create(response.ResponseModelInterface);
        }

        public static async Task<IOfferDetailedModel> GetOfferDetails(
            DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters parameters)
        {
            var response =
                await new DetailedOfferGetProcessor(parameters).SendRequest("Offer details was retrieved successfully");
            return response.ResponseModelInterface;
        }
    }
}