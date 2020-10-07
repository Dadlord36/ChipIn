using Common;
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
    public class ReservedSponsorsAdPostersListAdapter : SelectableElementsPagesListAdapter<ReservedSponsorsAdPostersRepository, SponsoredPosterDataModel,
        DefaultFillingViewPageViewHolder<SponsoredAdCardViewModel.FieldFillingData, uint>, uint, SponsoredAdCardViewModel.FieldFillingData,
        ReservedSponsorsAdPostersListAdapter.FillingViewAdapter>
    {
        public class FillingViewAdapter : FillingViewAdapter<SponsoredPosterDataModel, SponsoredAdCardViewModel.FieldFillingData>
        {
            public override SponsoredAdCardViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                SponsoredPosterDataModel data, uint dataIndexInRepository)
            {
                return new SponsoredAdCardViewModel.FieldFillingData(data.BackgroundUrl, data.LogoUrl);
            }
        }

        protected override void OnScrollPositionChanged(double normPos)
        {
            base.OnScrollPositionChanged(normPos);
            if (!IsInitialized || VisibleItemsCount == 0)
                return;
            FindMiddleElement();
        }
    }
}