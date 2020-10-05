using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(ScriptableSponsoredAdRepository), menuName = nameof(Repositories) + "/" + nameof(Remote) + "/"
                                                                                    + nameof(Paginated) + "/" + nameof(ScriptableSponsoredAdRepository),
        order = 0)]
    public class ScriptableSponsoredAdRepository : ScriptablePaginatedItemsListRepository<SponsoredAdDataModel, SponsoredAdvertsResponseDataModel,
        ISponsoredAdvertsResponseModel,SponsoredAdRepository>
    {
    }
}