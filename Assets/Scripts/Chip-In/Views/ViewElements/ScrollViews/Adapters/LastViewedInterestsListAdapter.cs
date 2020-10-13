using Com.TheFallenGames.OSA.CustomParams;
using DataModels;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class LastViewedInterestsListAdapter : SelectableListViewAdapter<BaseParamsWithPrefab, InterestBasicDataModel>
    {
    }
}