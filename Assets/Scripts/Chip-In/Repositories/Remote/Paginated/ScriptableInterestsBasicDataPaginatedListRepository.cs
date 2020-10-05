using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(Paginated) + "/" +
                                nameof(ScriptableInterestsBasicDataPaginatedListRepository),
        fileName = nameof(ScriptableInterestsBasicDataPaginatedListRepository), order = 0)]
    public class ScriptableInterestsBasicDataPaginatedListRepository : ScriptablePaginatedItemsListRepository<InterestBasicDataModel,
        CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse, InterestsBasicDataPaginatedListRepository>
    {
    }
}