using System.Net;
using System.Threading.Tasks;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using RestSharp;
using Utilities;
using ViewModels;

namespace RequestsStaticProcessors
{
    public static class AdvertStaticRequestsProcessor
    {
        private const string Tag = nameof(AdvertStaticRequestsProcessor);

        public static Task<BaseRequestProcessor<object, AdvertsListResponseDataModel, IAdvertsListResponseModel>.HttpResponse>
            ListAdverts(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, PaginatedRequestData
                paginatedRequestData)
        {
            return new AdvertsListGetRequestProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData).SendRequest(
                "Adverts list was retrieved successfully");
        }

        public static Task<IRestResponse> CreateAnAdvert(IRequestHeaders requestHeaders, CompanyAdFeaturesPreviewData companyAdFeaturesPreviewData)
        {
            var client = new RestClient("http://chip-in-dev.herokuapp.com/api/v1/adverts") {Timeout = -1};
            client.ClearHandlers();

            var request = new RestRequest(Method.POST);


            request.AddHeader(HttpRequestHeader.Accept.ToString(), ApiHelper.JsonMediaTypeHeader);
            request.AddHeader(HttpRequestHeader.ContentType.ToString(), ApiHelper.MultipartFormData);
            request.AddHeaders(requestHeaders.GetRequestHeaders());

            AddAdvertFileParam("poster", companyAdFeaturesPreviewData.CompanyLogoImagePath);
            AddAdvertFileParam("logo", companyAdFeaturesPreviewData.CompanyPosterImagePath);

            {
                var featureModels = companyAdFeaturesPreviewData.FeatureModelsToPreview;
                for (int i = 0; i < featureModels.Length; i++)
                {
                    AddAdvertFeaturesAttributeParameter("description", i, featureModels[i].Description);
                    AddAdvertFeaturesAttributeParameter("tokens_amount", i, featureModels[i].TokensRewardAmount.ToString());
                    AddAdvertFeaturesAttributeFile("icon", i, featureModels[i].PosterImagePath);
                }
            }

            const string advert = "advert";

            void AddAdvertFileParam(string hierarchy, in string value)
            {
                request.AddFile($"{advert}{WrapChild(hierarchy)}", value);
            }

            void AddAdvertParam(string hierarchy, in string value)
            {
                request.AddParameter($"{advert}{WrapChild(hierarchy)}", value);
            }

            string WrapChild(in string text)
            {
                var extra = string.Empty;
                if (!string.IsNullOrEmpty(text))
                {
                    extra = $"[{text}]";
                }

                return extra;
            }

            void AddAdvertFeaturesAttributeFile(string parameterName, int index, in string value)
            {
                request.AddFile(CreateAdvertFeatureArrayParameterName(parameterName, index), value);
            }

            void AddAdvertFeaturesAttributeParameter(string parameterName, int index, in string value)
            {
                request.AddParameter(CreateAdvertFeatureArrayParameterName(parameterName, index), value);
            }

            string CreateAdvertFeatureArrayParameterName(in string parameterName, int index)
            {
                return $"{advert}[advert_features_attributes][{index}][{parameterName}]";
            }

            var result = request.ToString();
            LogUtility.PrintLog(Tag, result);
            return client.ExecuteAsync(request);
        }


        /*public static Task  CreateAnAdvert(DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, 
            CompanyAdFeaturesPreviewData companyAdFeaturesPreviewData)
        {
            FilesUtility.ReadFileBytesAsync();
            var imageAsBytesArray = await ;
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

            try
            {
                var formAsString = await form.ReadAsStringAsync().ConfigureAwait(false);
                LogUtility.PrintLog(Tag, formAsString);

                var requestTask = ApiHelper.MakeAsyncMultiPartRequest(cancellationTokenSource.Token, HttpMethod.Post,
                    ApiCategories.Offers, form, requestHeaders.GetRequestHeaders());

                using (var response = await requestTask.ConfigureAwait(false))
                {
                    LogUtility.PrintLog(Tag, $"Response phrase: {response.ReasonPhrase}");
                    LogUtility.PrintLog(Tag, $"Response request message: {response.RequestMessage}");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        LogUtility.PrintLog(Tag, responseAsString);

                        return JsonConverterUtility.ConvertJsonString<ChallengingOfferWithIdentifierModel>(responseAsString);
                    }
                    else
                    {
                        var asString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        LogUtility.PrintLogError(Tag, "Offer was not created");
                        LogUtility.PrintLog(Tag, $"Response body: {asString}");
                    }
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }*/
    }
}