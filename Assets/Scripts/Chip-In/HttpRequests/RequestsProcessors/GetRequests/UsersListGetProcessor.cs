using System.Net.Http;
using System.Threading;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UsersListGetProcessor : RequestWithoutBodyProcessor<UsersListResponseDataModel, IUserListResponseModel>
    {
        public UsersListGetProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource, ApiCategories.Users, HttpMethod.Get,
            requestHeaders, null, paginatedRequestData.ConvertPaginationToNameValueCollection())
        {
        }
    }
}