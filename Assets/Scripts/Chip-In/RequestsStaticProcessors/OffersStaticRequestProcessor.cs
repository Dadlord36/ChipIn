using System;
using System.IO;
using System.Net;
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
using Repositories.Interfaces;
using Repositories.Remote;
using RestSharp;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class OffersStaticRequestProcessor
    {
        private const string Tag = nameof(OffersStaticRequestProcessor);


        public static Task<BaseRequestProcessor<object, GetClientOffersResponseDataModel, IGetClientOffersResponseModel>.HttpResponse>
            GetClientsOffers(out DisposableCancellationTokenSource cancellationTokenSource, IUserAuthorisationDataRepository authorisationDataRepository,
                int interestId, PaginatedRequestData paginatedRequestData)
        {
            return new ClientsOffersGetProcessor(out cancellationTokenSource, authorisationDataRepository, paginatedRequestData, interestId)
                .SendRequest("Client offers was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, GetOwnerOffersResponseDataModel, IGetOwnerOffersResponseModel>.HttpResponse>
            GetOwnerOffers(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData)
        {
            return new OwnerOffersGetProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData)
                .SendRequest("Offers was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, OfferDetailsResponseModel, IOfferDetailsResponseModel>.HttpResponse>
            TryGetOfferDetails(out DisposableCancellationTokenSource cancellationTokenSource,
                DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters parameters)
        {
            return new DetailedOfferGetProcessor(out cancellationTokenSource, parameters)
                .SendRequest("Offer details was retrieved successfully");
        }

        public static async Task<CreateAnOfferResponseModel> TryCreateAnOffer(CancellationTokenSource cancellationTokenSource,
            IRequestHeaders requestHeaders, IOfferCreationRequestModel requestModel)
        {
            var elements = DataModelsUtility.ToKeyValue(requestModel.Offer);

            var form = new MultipartFormDataContent
            {
                {
                    new ByteArrayContent(File.ReadAllBytes(requestModel.PosterImageFilePath)), "offer[poster]",
                    Path.GetFileName(requestModel.PosterImageFilePath)
                }
            };

            foreach (var element in elements)
            {
                form.Add(new StringContent(element.Value), $"offer[{element.Key}]");
            }

            try
            {
                var formAsString = await form.ReadAsStringAsync().ConfigureAwait(false);
                LogUtility.PrintLog(Tag, formAsString);

                var requestTask = await ApiHelper.MakeAsyncMultiPartRequest(cancellationTokenSource.Token, HttpMethod.Post,
                    ApiCategories.Offers, form, requestHeaders.GetRequestHeaders()).ConfigureAwait(false);

                using (var response = await requestTask.ConfigureAwait(false))
                {
                    LogUtility.PrintLog(Tag, $"Response phrase: {response.ReasonPhrase}");
                    LogUtility.PrintLog(Tag, $"Response request message: {response.RequestMessage}");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        LogUtility.PrintLog(Tag, responseAsString);

                        return JsonConverterUtility.ConvertJsonString<CreateAnOfferResponseModel>(responseAsString);
                    }
                    else
                    {
                        var asString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        LogUtility.PrintLogError(Tag, "Offer was not created");
                        LogUtility.PrintLog(Tag, $"Response body: {asString}");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }


        public static Task<IRestResponse> CreateFlashOffer(CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            IFlashOfferCreationRequestModel requestModel)
        {
            var client = new RestClient(ApiHelper.ApiUrl + ApiCategories.FlashOffers) {Timeout = -1};
            client.ClearHandlers();

            var request = new RestRequest(Method.POST);


            request.AddHeader(HttpRequestHeader.Accept.ToString(), ApiHelper.JsonMediaTypeHeader);
            request.AddHeader(HttpRequestHeader.ContentType.ToString(), ApiHelper.MultipartFormData);
            request.AddHeaders(requestHeaders.GetRequestHeaders());

            var elements = DataModelsUtility.ToKeyValue(requestModel.FlashOffer);

            foreach (var element in elements)
            {
                request.AddParameter($"flash_offer[{element.Key}]", element.Value);
            }

            request.AddFile("flash_offer[poster]", requestModel.PosterFilePath.Path);
            var result = request.ToString();

            LogUtility.PrintLog(Tag, result);
            return client.ExecuteAsync(request, cancellationTokenSource.Token);
        }

        public static Task<BaseRequestProcessor<object, OfferDetailsResponseModel, IOfferDetailsResponseModel>.HttpResponse>
            GetOfferDetails(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int? offerId)
        {
            return TryGetOfferDetails(out cancellationTokenSource, new DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters(requestHeaders,
                offerId));
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            BuyAnOffer(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int offerId)
        {
            return new BuyPurchasePostProcessor(out cancellationTokenSource, requestHeaders, offerId)
                .SendRequest("Offer was bought successfully");
        }
    }
}