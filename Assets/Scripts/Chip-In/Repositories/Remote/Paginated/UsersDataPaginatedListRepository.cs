using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using RequestsStaticProcessors;

namespace Repositories.Remote.Paginated
{
    public class UsersDataPaginatedListRepository : PaginatedItemsListRepository<UserProfileBaseData, UsersListResponseDataModel,
        IUserListResponseModel>
    {
        public UsersDataPaginatedListRepository() : base(nameof(UsersDataPaginatedListRepository))
        {
        }

        protected override Task<BaseRequestProcessor<object, UsersListResponseDataModel, IUserListResponseModel>.HttpResponse> CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData)
        {
            return UsersRequestsStaticProcessor.GetUsersList(out cancellationTokenSource, AuthorisationDataRepositoryHeaders, paginatedRequestData);
        }

        protected override List<UserProfileBaseData> GetItemsFromResponseModelInterface(IUserListResponseModel responseModelInterface)
        {
            return new List<UserProfileBaseData>(responseModelInterface.UsersData);
        }
    }
}