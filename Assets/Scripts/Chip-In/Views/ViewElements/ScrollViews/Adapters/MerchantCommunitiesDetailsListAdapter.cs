using DataModels;
using Repositories.Remote.Paginated;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class MerchantCommunitiesDetailsListAdapter : SelectableElementsPagesListAdapter<MarketInterestsPaginatedListRepository,
        MarketInterestDetailsDataModel>
    {
    }
}