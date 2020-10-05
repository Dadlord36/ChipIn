using DataModels;
using DataModels.ResponsesModels;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(ScriptableUsersDataPaginatedListRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                                                                                               + nameof(ScriptableUsersDataPaginatedListRepository),
        order = 0)]
    public class ScriptableUsersDataPaginatedListRepository : ScriptablePaginatedItemsListRepository<UserProfileBaseData, UsersListResponseDataModel,
        IUserListResponseModel,UsersDataPaginatedListRepository>
    {
    }
}