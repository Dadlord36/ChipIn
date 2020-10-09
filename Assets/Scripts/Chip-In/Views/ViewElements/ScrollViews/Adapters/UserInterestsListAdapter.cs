using DataModels;
using Repositories.Remote.Paginated;
using UnityWeld.Binding;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class UserInterestsListAdapter : SelectableElementsPagesListAdapter<InterestsBasicDataPaginatedListRepository, InterestBasicDataModel,
        DefaultFillingViewPageViewHolder<InterestItemViewModel.FieldFillingData, InterestBasicDataModel>, InterestBasicDataModel,
        InterestItemViewModel.FieldFillingData, UserInterestsLabelFillingViewAdapter>
    {
    }
}