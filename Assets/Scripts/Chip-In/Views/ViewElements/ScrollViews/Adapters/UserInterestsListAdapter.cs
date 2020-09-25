using DataModels;
using Repositories.Remote.Paginated;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class UserInterestsListAdapter : SelectableElementsPagesListAdapter<InterestsBasicDataPaginatedListRepository, InterestBasicDataModel,
        DefaultFillingViewPageViewHolder<InterestItemViewModel.FieldFillingData>, InterestItemViewModel.FieldFillingData, UserInterestsLabelFillingViewAdapter>
    {
    }
}