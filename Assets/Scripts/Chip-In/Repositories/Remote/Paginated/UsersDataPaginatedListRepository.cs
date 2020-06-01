using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class UsersDataPaginatedListRepository : BasePaginatedItemsListRepository<UserProfileBaseData,
        UsersListResponseDataModel, IUserListResponseModel>
    {
        protected override string Tag =>nameof(UsersDataPaginatedListRepository);
       

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }

        protected override List<UserProfileBaseData> GetItemsFromResponseModelInterface(
            IUserListResponseModel responseModelInterface)
        {
            return new List<UserProfileBaseData>(responseModelInterface.UsersData);
        }


        protected override
            Task<BaseRequestProcessor<object, UsersListResponseDataModel, IUserListResponseModel>.HttpResponse>
            CreateLoadPaginatedItemsTask(PaginatedRequestData paginatedRequestData)
        {
            return UsersRequestsStaticProcessor.GetUsersList(authorisationDataRepository, paginatedRequestData);
        }
    }
}