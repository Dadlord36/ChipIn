using Com.TheFallenGames.OSA.CustomParams;
using Common;
using DataModels;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class UserInterestsLabelsSimpleListAdapter : BasedListAdapter<BaseParamsWithPrefab, DefaultFillingViewPageViewHolder<InterestItemViewModel.FieldFillingData>,
        InterestBasicDataModel, InterestItemViewModel.FieldFillingData, UserInterestsLabelsSimpleListAdapter.UserInterestsLabelsFillingViewAdapter>
    {
        public class UserInterestsLabelsFillingViewAdapter : FillingViewAdapter<InterestBasicDataModel, InterestItemViewModel.FieldFillingData>
        {
            public override InterestItemViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource, InterestBasicDataModel data,
                uint dataIndexInRepository)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}