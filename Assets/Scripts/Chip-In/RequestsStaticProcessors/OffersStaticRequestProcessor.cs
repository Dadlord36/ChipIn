using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
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

        public static async Task<LiveData<ChallengingOfferWithIdentifierModel>> TryGetListOfOffers(IRequestHeaders requestHeaders)
        {
            try
            {
                var response = await new OffersGetProcessor(requestHeaders).SendRequest("Offers was retrieved successfully");

                LiveData<ChallengingOfferWithIdentifierModel> Create(IOffersResponseModel offersResponseModel)
                {
                    return new LiveData<ChallengingOfferWithIdentifierModel>(offersResponseModel.Offers);
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
            var imageAsBytesArray = File.ReadAllBytes(requestModel.PosterImageFilePath) ;
            var elements = DataModelsUtility.ToKeyValue(requestModel.Offer);

            var form = new MultipartFormDataContent {{new ByteArrayContent(imageAsBytesArray), "offer[poster]",
                Path.GetFileName(requestModel.PosterImageFilePath)}};

            foreach (var element in elements)
            {
                form.Add(new StringContent(element.Value), $"offer[{element.Key}]");
            }


            var formAsString = await form.ReadAsStringAsync();
            LogUtility.PrintLog(Tag, formAsString);


            using (var response = await ApiHelper.MakeAsyncMultiPartRequest(HttpMethod.Post, ApiCategories.Offers, form,
                requestHeaders.GetRequestHeaders()))
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
                    LogUtility.PrintLogError(Tag, "Offer was not created");
                    LogUtility.PrintLog(Tag, $"Response body: {asString}");
                }
            }

            return null;
        }

        public static Task<IOfferDetailsResponseModel> GetOfferDetails(IRequestHeaders requestHeaders, int? offerId)
        {
            return TryGetOfferDetails(new DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters(requestHeaders, offerId));
        }
    }
}