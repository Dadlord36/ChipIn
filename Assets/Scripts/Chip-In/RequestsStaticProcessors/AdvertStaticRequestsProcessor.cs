using System.Net.Http;
using System.Threading.Tasks;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using Factories;
using GlobalVariables;
using HttpRequests;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using RestSharp;
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
            return new AdvertsListGetRequestProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData)
                .SendRequest("Adverts list was retrieved successfully");
        }

        public static Task<IRestResponse> CreateAnAdvert(IRequestHeaders requestHeaders, CompanyAdFeaturesPreviewData companyAdFeaturesPreviewData)
        {
            var request = RequestsFactory.MultipartRestRequest(requestHeaders, Method.POST, ApiCategories.Adverts);
            AddAdvertFileParam(MainNames.ModelsPropertiesNames.Poster, companyAdFeaturesPreviewData.CompanyLogoImagePath);
            AddAdvertFileParam(MainNames.ModelsPropertiesNames.Logo, companyAdFeaturesPreviewData.CompanyPosterImagePath);

            //ToDo: Check REST API Docs if this parameter was removed as it should be.
            AddAdvertParam(MainNames.ModelsPropertiesNames.InterestId, "425");

            {
                var featureModels = companyAdFeaturesPreviewData.FeatureModelsToPreview;
                for (int i = 0; i < featureModels.Count; i++)
                {
                    AddAdvertFeaturesAttributeParameter(MainNames.ModelsPropertiesNames.Description, i, featureModels[i].Description);
                    AddAdvertFeaturesAttributeParameter(MainNames.ModelsPropertiesNames.TokensAmount, i, featureModels[i].TokensAmount.ToString());
                    AddAdvertFeaturesAttributeFile(MainNames.ModelsPropertiesNames.Icon, i, featureModels[i].Icon);
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

            return ApiHelper.ExecuteRequestWithDefaultRestClient(request);
        }

        public static Task<BaseRequestProcessor<object, SponsoredAdvertsResponseDataModel, ISponsoredAdvertsResponseModel>.HttpResponse>
            GetListOfSponsoredAdverts(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData)
        {
            return new SponsoredAdvertsGetRequestProcessor(out cancellationTokenSource, ApiCategories.SponsoredAd, HttpMethod.Get, requestHeaders,
                paginatedRequestData).SendRequest("Sponsored adverts was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, SponsorsPostersResponseDataModel, ISponsorsPostersResponseModel>.HttpResponse>
            GetListOfSponsorsPosters(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData, bool sad, bool reserve)
        {
            return new SponsoredPostersGetProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData, sad, reserve)
                .SendRequest("Sponsors posters retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, SponsorsPostersResponseDataModel, ISponsorsPostersResponseModel>.HttpResponse>
            GetAllSponsoredAdPosters(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData)
        {
            return new SponsoredPostersGetProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData, true, false)
                .SendRequest("Sponsored Adverts posters retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, SponsorsPostersResponseDataModel, ISponsorsPostersResponseModel>.HttpResponse>
            GetAllReservedAdPosters(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData)
        {
            return new SponsoredPostersGetProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData, false, true)
                .SendRequest("Sponsored Adverts posters retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, SponsorsPostersResponseDataModel, ISponsorsPostersResponseModel>.HttpResponse>
            GetAllGreetingsPosters(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData)
        {
            return new SponsoredPostersGetProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData, false, false)
                .SendRequest("Sponsored Adverts posters retrieved successfully");
        }
    }
}