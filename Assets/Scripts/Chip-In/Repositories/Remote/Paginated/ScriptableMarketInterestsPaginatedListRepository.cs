using DataModels;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using UnityEngine;

namespace Repositories.Remote.Paginated
{
    [CreateAssetMenu(fileName = nameof(ScriptableMarketInterestsPaginatedListRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + "/" + nameof(Paginated) + nameof(ScriptableMarketInterestsPaginatedListRepository),
        order = 0)]
    public class ScriptableMarketInterestsPaginatedListRepository : ScriptablePaginatedItemsListRepository<MarketInterestDetailsDataModel,
        MerchantInterestsResponseDataModel, IMerchantInterestsResponseModel, MarketInterestsPaginatedListRepository>
    {
    }
}