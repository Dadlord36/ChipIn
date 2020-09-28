using System;
using Common;
using DataModels;
using Repositories.Remote.Paginated;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class SponsorsAdPostersListAdapter : SelectableElementsPagesListAdapter<SponsorsAdPostersRepository, SponsoredPosterDataModel,
        DefaultFillingViewPageViewHolder<SponsoredAdCardViewModel.FieldFillingData>, SponsoredAdCardViewModel.FieldFillingData,
        SponsorsAdPostersListAdapter.SponsoredAdFillingViewAdapter>
    {
        public class SponsoredAdFillingViewAdapter : FillingViewAdapter<SponsoredPosterDataModel, SponsoredAdCardViewModel.FieldFillingData>
        {
            private const string Tag = nameof(SponsoredAdFillingViewAdapter);

            public override SponsoredAdCardViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                SponsoredPosterDataModel data, uint dataIndexInRepository)
            {
                try
                {
                    return new SponsoredAdCardViewModel.FieldFillingData(DownloadedSpritesRepository.CreateLoadSpriteTask(data.BackgroundUrl,
                        cancellationTokenSource.Token), DownloadedSpritesRepository.CreateLoadSpriteTask(data.LogoUrl, cancellationTokenSource.Token));
                }
                catch (OperationCanceledException)
                {
                    LogUtility.PrintDefaultOperationCancellationLog(Tag);
                    throw;
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                    throw;
                }
            }
        }
    }
}