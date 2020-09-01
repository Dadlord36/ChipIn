using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(UsersDataPaginatedListRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UsersDataPaginatedListRepository),
        order = 0)]
    public class UsersDataPaginatedListRepository : PaginatedItemsListRepository<UserProfileBaseData,
        UsersListResponseDataModel, IUserListResponseModel>
    {
        protected override string Tag => nameof(UsersDataPaginatedListRepository);
        

        protected override Task<BaseRequestProcessor<object, UsersListResponseDataModel, IUserListResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource,
                PaginatedRequestData paginatedRequestData)
        {
            return UsersRequestsStaticProcessor.GetUsersList(out cancellationTokenSource, AuthorisationDataRepository,
                paginatedRequestData);
        }

        protected override List<UserProfileBaseData> GetItemsFromResponseModelInterface(
            IUserListResponseModel responseModelInterface)
        {
            return new List<UserProfileBaseData>(responseModelInterface.UsersData);
        }
    }
}