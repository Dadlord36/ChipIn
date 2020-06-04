using System;
using System.IO;
using System.Net.Http;
using System.Threading;
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
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class OffersStaticRequestProcessor
    {
        private const string Tag = nameof(OffersStaticRequestProcessor);

        public static Task<BaseRequestProcessor<object, OffersResponseModel, IOffersResponseModel>.HttpResponse> TryGetListOfOffers(
            out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            return new OffersGetProcessor(out cancellationTokenSource, requestHeaders)
                .SendRequest("Offers was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, OfferDetailsResponseModel, IOfferDetailsResponseModel>.HttpResponse>
            TryGetOfferDetails(out CancellationTokenSource cancellationTokenSource,
                DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters parameters)
        {
            return new DetailedOfferGetProcessor(out cancellationTokenSource, parameters).SendRequest(
                "Offer details was retrieved successfully");
        }

        public static async Task<ChallengingOfferWithIdentifierModel> TryCreateAnOffer(CancellationTokenSource cancellationTokenSource,
            IRequestHeaders requestHeaders, IOfferCreationRequestModel requestModel)
        {
            var imageAsBytesArray = File.ReadAllBytes(requestModel.PosterImageFilePath);
            var elements = DataModelsUtility.ToKeyValue(requestModel.Offer);

            var form = new MultipartFormDataContent
            {
                {
                    new ByteArrayContent(imageAsBytesArray), "offer[poster]",
                    Path.GetFileName(requestModel.PosterImageFilePath)
                }
            };

            foreach (var element in elements)
            {
                form.Add(new StringContent(element.Value), $"offer[{element.Key}]");
            }


            var formAsString = await form.ReadAsStringAsync();
            LogUtility.PrintLog(Tag, formAsString);


            using (var response = await await ApiHelper.MakeAsyncMultiPartRequest(cancellationTokenSource.Token, HttpMethod.Post,
                ApiCategories.Offers, form, requestHeaders.GetRequestHeaders()))
            {
                LogUtility.PrintLog(Tag, $"Response phrase: {response.ReasonPhrase}");
                LogUtility.PrintLog(Tag, $"Response request message: {response.RequestMessage}");
                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    LogUtility.PrintLog(Tag, responseAsString);


                    return JsonConverterUtility
                        .ConvertJsonString<ChallengingOfferWithIdentifierModel>(responseAsString);
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

        public static Task<BaseRequestProcessor<object, OfferDetailsResponseModel, IOfferDetailsResponseModel>.HttpResponse>
            GetOfferDetails(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int? offerId)
        {
            return TryGetOfferDetails(out cancellationTokenSource, new DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters(requestHeaders, offerId));
        }
    }
}