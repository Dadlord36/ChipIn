using System;
using System.Threading.Tasks;
using DataModels;
using Repositories.Temporary;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public sealed class ConnectViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private CompanyAdListAdapter companyAdListAdapter;
        [SerializeField] private SponsoredAdRepository sponsoredAdRepository;
        [SerializeField] private SponsoredAdRepository reservedSponsoredAdRepository;

        [SerializeField] private SponsoredAdListAdapter sponsoredAdListAdapter;
        [SerializeField] private SponsoredAdListAdapter reservedSponsoredAdListAdapter;


        private uint selectedSponsoredAdId;
        private uint selectedReservedAdId;

        [Binding]
        public uint SelectedSponsoredAdId
        {
            get => selectedSponsoredAdId;
            set => SelectNewSponsoredAd(selectedSponsoredAdId = value);
        }

        [Binding]
        public uint SelectedReservedAdId
        {
            get => selectedReservedAdId;
            set => SelectNewReservedAd(selectedReservedAdId = value);
        }


        public ConnectViewModel() : base(nameof(ConnectViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await Task.WhenAll(companyAdListAdapter.ResetAsync(), sponsoredAdListAdapter.ResetAsync(), reservedSponsoredAdListAdapter.ResetAsync())
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

        private async void SelectNewSponsoredAd(uint index)
        {
            try
            {
                var data = await sponsoredAdRepository.GetItemWithIndexAsync(index).ConfigureAwait(false);

                SwitchToView(nameof(SponsoredAdView), new FormsTransitionBundle(data));
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        private async void SelectNewReservedAd(uint index)
        {
            try
            {
                SponsoredAdDataModel data = await reservedSponsoredAdRepository.GetItemWithIndexAsync(index).ConfigureAwait(false);

                SwitchToView(nameof(ReservedSponsoredAdView), new FormsTransitionBundle(data));
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        [Binding]
        public void CreateAdButton_OnClick()
        {
            SwitchToView(nameof(CreateCompanyAdView));
        }
    }
}