using System;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public class EngageViewModel : ViewsSwitchingViewModel
    {
        private int _selectedCommunityId;

        [Binding]
        public int SelectedCommunityId
        {
            get => _selectedCommunityId;
            set => MerchantInterestListAdapterOnItemSelected(_selectedCommunityId = value);
        }

        public EngageViewModel() : base(nameof(EngageViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            var adapter = GetComponentInChildren<MerchantCommunitiesDetailsListAdapter>();
            try
            {
                IsAwaitingProcess = true;
                await adapter.ResetAsync().ConfigureAwait(true);
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
            finally
            {
                IsAwaitingProcess = false;
            }
        }


        private void MerchantInterestListAdapterOnItemSelected(int index)
        {
            SwitchToView(nameof(MerchantInterestView), new FormsTransitionBundle(index));
        }
    }
}