using Common;
using DataModels;
using Repositories.Temporary;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class SponsoredAdFullListAdapter : SelectableElementsPagesListAdapter<SponsoredAdRepository, SponsoredAdDataModel,
        DefaultFillingViewPageViewHolder<SponsoredAdFullCardViewModel.FieldFillingData>, SponsoredAdFullCardViewModel.FieldFillingData,
        SponsoredAdFullListAdapter.FillingViewAdapter>
    {
        public class FillingViewAdapter : FillingViewAdapter<SponsoredAdDataModel, SponsoredAdFullCardViewModel.FieldFillingData>
        {
            public override SponsoredAdFullCardViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                SponsoredAdDataModel data, uint dataIndexInRepository)
            {
                return new SponsoredAdFullCardViewModel.FieldFillingData(DownloadedSpritesRepository.CreateLoadSpriteTask(data.PosterUri,
                    cancellationTokenSource.Token));
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