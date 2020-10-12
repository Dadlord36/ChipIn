using Com.TheFallenGames.OSA.CustomParams;
using Common;
using DataModels;
using UnityWeld.Binding;
using ViewModels.Elements;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class LastViewedInterestsListAdapter : SelectableListViewAdapter<BaseParamsWithPrefab, InterestBasicDataModel,
        DefaultFillingViewPageViewHolder<ImageWithTextViewModel.FieldFillingData, InterestBasicDataModel>, InterestBasicDataModel,
        ImageWithTextViewModel.FieldFillingData, LastViewedInterestsListAdapter.FillingViewAdapter>
    {
        public class FillingViewAdapter : FillingViewAdapter<InterestBasicDataModel, ImageWithTextViewModel.FieldFillingData>
        {
            public override ImageWithTextViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                InterestBasicDataModel data, uint dataIndexInRepository)
            {
                return new ImageWithTextViewModel.FieldFillingData(data);
            }
        }
    }
}