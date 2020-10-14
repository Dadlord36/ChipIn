using System.Collections.Specialized;
using System.Net.Http;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class FavoriteInterestsGetProcessor : RequestWithoutBodyProcessor<UserInterestPagesResponseDataModel, IUserInterestPagesResponseModel>
    {
        public FavoriteInterestsGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData, in string interestName) : base(out cancellationTokenSource,
            ApiCategories.Subcategories.Interests, HttpMethod.Get, requestHeaders, new[] {ApiCategories.Subcategories.Favorites},
            paginatedRequestData, new NameValueCollection
            {
                {MainNames.ModelsPropertiesNames.Name, interestName}
            })
        {
        }

        public FavoriteInterestsGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource,
            ApiCategories.Subcategories.Interests, HttpMethod.Get, requestHeaders, new[] {ApiCategories.Subcategories.Favorites},
            paginatedRequestData)
        {
        }
    }
}