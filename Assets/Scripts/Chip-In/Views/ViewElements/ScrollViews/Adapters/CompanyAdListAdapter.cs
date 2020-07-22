using System;
using Common;
using DataModels;
using Repositories.Remote.Paginated;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class CompanyAdListAdapter : RepositoryBasedListAdapter<AdvertsPaginatedListRepository, AdvertItemDataModel,
        DefaultFillingViewPageViewHolder<AdCardViewModel.FieldFillingData>, AdCardViewModel.FieldFillingData,
        CompanyAdListAdapter.CompanyAdFillingViewAdapter>
    {

        public class CompanyAdFillingViewAdapter : FillingViewAdapter<AdvertItemDataModel, AdCardViewModel.FieldFillingData>
        {
            private const string Tag = nameof(CompanyAdFillingViewAdapter);

            public override AdCardViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                AdvertItemDataModel data, uint dataIndexInRepository)
            {
                try
                {
                    return new AdCardViewModel.FieldFillingData(DownloadedSpritesRepository.CreateLoadTexture2DTask(data.PosterUri,
                        cancellationTokenSource.Token), data.Slogan);
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