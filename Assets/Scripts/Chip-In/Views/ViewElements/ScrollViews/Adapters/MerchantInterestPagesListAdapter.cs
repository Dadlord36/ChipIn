using DataModels;
using Repositories.Remote.Paginated;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public sealed class MerchantInterestPagesListAdapter : SelectableElementsPagesListAdapter<MerchantInterestPagesPaginatedRepository,
        MerchantInterestPageDataModel>
    {
    }
}