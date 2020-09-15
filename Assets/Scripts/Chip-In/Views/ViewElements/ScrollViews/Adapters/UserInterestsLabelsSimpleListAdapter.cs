using Com.TheFallenGames.OSA.CustomParams;
using DataModels;
using UnityWeld.Binding;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class UserInterestsLabelsSimpleListAdapter : SelectableListViewAdapter<BaseParamsWithPrefab, InterestBasicDataModel,
        DefaultFillingViewPageViewHolder<InterestItemViewModel.FieldFillingData>, InterestItemViewModel.FieldFillingData, 
        UserInterestsLabelFillingViewAdapter>
    {

    }
}