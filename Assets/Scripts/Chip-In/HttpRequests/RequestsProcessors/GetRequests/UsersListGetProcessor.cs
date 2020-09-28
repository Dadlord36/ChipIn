using System.Net.Http;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UsersListGetProcessor : RequestWithoutBodyProcessor<UsersListResponseDataModel, IUserListResponseModel>
    {
        public UsersListGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource, ApiCategories.Users, HttpMethod.Get,
            requestHeaders, paginatedRequestData)
        {
        }
    }
}