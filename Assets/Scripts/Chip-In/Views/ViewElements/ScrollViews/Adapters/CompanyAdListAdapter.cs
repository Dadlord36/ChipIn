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
    public class CompanyAdListAdapter : SelectableElementsPagesListAdapter<AdvertsPaginatedListRepository, AdvertItemDataModel,
        DefaultFillingViewPageViewHolder<AdCardViewModel.FieldFillingData>, AdCardViewModel.FieldFillingData,
        CompanyAdListAdapter.FillingViewAdapter>
    {
        public class FillingViewAdapter : FillingViewAdapter<AdvertItemDataModel, AdCardViewModel.FieldFillingData>
        {
            private const string Tag = nameof(FillingViewAdapter);

            public override AdCardViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                AdvertItemDataModel data, uint dataIndexInRepository)
            {
                try
                {
                    return new AdCardViewModel.FieldFillingData(data.PosterUri, data.Slogan);
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