using DataModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(ScriptableReservedSponsorsAdPostersRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(Paginated) + "/" + nameof(ScriptableReservedSponsorsAdPostersRepository),
        order = 0)]
    public class ScriptableReservedSponsorsAdPostersRepository : ScriptablePaginatedItemsListRepository<SponsoredPosterDataModel,
        SponsorsPostersResponseDataModel, ISponsorsPostersResponseModel,ReservedSponsorsAdPostersRepository>
    {
    }
}