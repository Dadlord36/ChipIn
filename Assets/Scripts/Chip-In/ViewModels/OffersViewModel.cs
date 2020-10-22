using System;
using Repositories.Remote;
using UnityEngine;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    public class OffersViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private ClientOffersRemoteRepository clientOffersRemoteRepository;
        [SerializeField] private ClientOffersListAdapter clientOffersListAdapter;

        public OffersViewModel() : base(nameof(OffersViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                clientOffersRemoteRepository.InterestId = (int) View.FormTransitionBundle.TransitionData;
                
                await clientOffersListAdapter.ResetAsync().ConfigureAwait(false);
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