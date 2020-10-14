using System;
using UnityEngine;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    public class FavoriteInterestsViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private FavoriteInterestsListAdapter favoriteInterestsListAdapter;

        public FavoriteInterestsViewModel() : base(nameof(FavoriteInterestsViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await favoriteInterestsListAdapter.ResetAsync()
                    .ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}