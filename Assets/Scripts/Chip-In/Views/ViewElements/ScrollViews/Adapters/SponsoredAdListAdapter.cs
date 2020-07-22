﻿using System;
using Common;
using DataModels;
using Repositories.Temporary;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Cards;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class SponsoredAdListAdapter : RepositoryBasedListAdapter<SponsoredAdRepository, SponsoredAdDataModel,
        DefaultFillingViewPageViewHolder<SponsoredAdCardViewModel.FieldFillingData>,
        SponsoredAdCardViewModel.FieldFillingData, SponsoredAdListAdapter.SponsoredAdFillingViewAdapter>
    {
        public class SponsoredAdFillingViewAdapter : FillingViewAdapter<SponsoredAdDataModel, SponsoredAdCardViewModel.FieldFillingData>
        {
            private const string Tag = nameof(SponsoredAdFillingViewAdapter);

            public override SponsoredAdCardViewModel.FieldFillingData Convert(DisposableCancellationTokenSource cancellationTokenSource,
                SponsoredAdDataModel data, uint dataIndexInRepository)
            {
                try
                {
                    return new SponsoredAdCardViewModel.FieldFillingData(DownloadedSpritesRepository.CreateLoadTexture2DTask(data.PosterUri,
                        cancellationTokenSource.Token));
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