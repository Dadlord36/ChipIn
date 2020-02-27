using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DataModels;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using HttpRequests;
using HttpRequests.RequestsProcessors.GetRequests;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class OffersStaticRequestProcessor
    {
        private const string Tag = nameof(OffersStaticRequestProcessor);
        public static async Task<PaginatedList<ChallengingOfferWithIdentifierModel>> TryGetListOfOffers(IRequestHeaders requestHeaders)
        {
            try
            {
                var response = await new OffersGetProcessor(requestHeaders).SendRequest("Offers was retrieved successfully");

                PaginatedList<ChallengingOfferWithIdentifierModel> Create(IOffersResponseModel offersResponseModel)
                {
                    return new PaginatedList<ChallengingOfferWithIdentifierModel>(offersResponseModel.Pagination, offersResponseModel.Offers);
                }

                return Create(response.ResponseModelInterface);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<IOfferDetailsResponseModel> TryGetOfferDetails(DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters parameters)
        {
            try
            {
                var response = await new DetailedOfferGetProcessor(parameters).SendRequest("Offer details was retrieved successfully");
                return response.ResponseModelInterface;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<ChallengingOfferWithIdentifierModel> TryCreateAnOffer(IRequestHeaders requestHeaders, 
            IOfferCreationRequestModel requestModel)
        {
            var filePath = requestModel.PosterFilePath.Path;
            var imageAsBytesArray = File.ReadAllBytes(filePath);
            var elements = DataModelsUtility.ToKeyValue(requestModel.Offer);
            
            var form = new MultipartFormDataContent {{new ByteArrayContent(imageAsBytesArray), "offer[poster]", Path.GetFileName(filePath)}};

            foreach (var element in elements)
            {
                form.Add(new StringContent(element.Value), $"offer[{element.Key}]");
            }


            var formAsString = await form.ReadAsStringAsync();
            LogUtility.PrintLog(Tag, formAsString);


            using (var response = await ApiHelper.MakeAsyncMultiPartRequest(HttpMethod.Post, RequestsSuffixes.Offers, form, requestHeaders.GetRequestHeaders()))
            {
                LogUtility.PrintLog(Tag, $"Response phrase: {response.ReasonPhrase}");
                LogUtility.PrintLog(Tag, $"Response request message: {response.RequestMessage}");
                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    LogUtility.PrintLog(Tag, responseAsString);


                    return JsonConverterUtility.ConvertJsonString<ChallengingOfferWithIdentifierModel>(responseAsString);
                }
                else
                {
                    var asString = await response.Content.ReadAsStringAsync();
                    LogUtility.PrintLog(Tag, $"Response body: {asString}");
                    throw new Exception("Offer was not created");
                }
            }
        }
    }
}