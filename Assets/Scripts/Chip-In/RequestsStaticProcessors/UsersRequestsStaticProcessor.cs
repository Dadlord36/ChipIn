using System.Threading.Tasks;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class UsersRequestsStaticProcessor
    {
        public static
            Task<BaseRequestProcessor<object, UsersListResponseDataModel, IUserListResponseModel>.HttpResponse>
            GetUsersList(out DisposableCancellationTokenSource cancellationTokensSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData)
        {
            return new UsersListGetProcessor(out cancellationTokensSource, requestHeaders, paginatedRequestData).SendRequest(
                "Users list was retrieved successfully");
        }
    }
}