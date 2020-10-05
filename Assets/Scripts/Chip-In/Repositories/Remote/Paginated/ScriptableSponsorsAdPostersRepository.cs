using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(ScriptableSponsorsAdPostersRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(Paginated) + "/" + nameof(ScriptableSponsorsAdPostersRepository), order = 0)]
    public class ScriptableSponsorsAdPostersRepository : ScriptablePaginatedItemsListRepository<SponsoredPosterDataModel, SponsorsPostersResponseDataModel,
        ISponsorsPostersResponseModel, SponsorsAdPostersRepository>
    {
    }
}