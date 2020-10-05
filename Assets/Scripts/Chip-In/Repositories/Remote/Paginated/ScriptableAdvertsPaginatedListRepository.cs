using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(Paginated) + "/" + "Create " +
                   nameof(ScriptableAdvertsPaginatedListRepository),
        fileName = nameof(ScriptableAdvertsPaginatedListRepository),
        order = 0)]
    public class ScriptableAdvertsPaginatedListRepository : ScriptablePaginatedItemsListRepository<AdvertItemDataModel, AdvertsListResponseDataModel,
        IAdvertsListResponseModel,AdvertsPaginatedListRepository>
    {
    }
}