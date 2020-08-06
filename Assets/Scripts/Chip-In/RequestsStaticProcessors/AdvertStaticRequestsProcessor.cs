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
    }
}