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
    public class CommunityClientsInterestsPaginatedGetProcessor : RequestWithoutBodyProcessor<UserInterestPagesResponseDataModel,
        IUserInterestPagesResponseModel>
    {
        public CommunityClientsInterestsPaginatedGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource,
            IRequestHeaders requestHeaders, int communityId, string categoryName, PaginatedRequestData paginatedRequestData)
            : base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Get, requestHeaders, new[]
                {
                    communityId.ToString(),
                    ApiCategories.Subcategories.Interests
                }, CreateNameValueCollection(categoryName, paginatedRequestData))
        {
        }

        private static NameValueCollection CreateNameValueCollection(in string categoryName, PaginatedRequestData paginatedRequestData)
        {
            var collection = paginatedRequestData.ConvertPaginationToNameValueCollection();
            collection.Add(MainNames.ModelsPropertiesNames.Category, categoryName);
            return collection;
        }
    }
}