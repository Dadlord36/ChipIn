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
    public class SponsoredAdFullListAdapter : SelectableElementsPagesListAdapter<SponsorsAdPostersRepository, SponsoredPosterDataModel,
        DefaultFillingViewPageViewHolder<SponsoredAdFullCardViewModel.FieldFillingData, uint>, uint, SponsoredAdFullCardViewModel.FieldFillingData,
        SponsoredAdFullListAdapter.FillingViewAdapter>
    {
        public class FillingViewAdapter : FillingViewAdapter<SponsoredPosterDataModel, SponsoredAdFullCardViewModel.FieldFillingData>
        {
            public override SponsoredAdFullCardViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                SponsoredPosterDataModel data, uint dataIndexInRepository)
            {
                return new SponsoredAdFullCardViewModel.FieldFillingData(DownloadedSpritesRepository.CreateLoadSpriteTask(data.BackgroundUrl,
                    cancellationTokenSource.Token), DownloadedSpritesRepository.CreateLoadSpriteTask(data.LogoUrl, cancellationTokenSource.Token));
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