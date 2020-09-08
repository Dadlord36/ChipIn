using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Repositories.Temporary;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public sealed class ConnectViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private CompanyAdListAdapter companyAdListAdapter;
        [SerializeField] private SponsoredAdRepository sponsoredAdRepository;

        [SerializeField] private SponsoredAdListAdapter sponsoredAdListAdapter;
        [SerializeField] private SponsoredAdListAdapter reservedSponsoredAdListAdapter;
        private uint _reservedSponsoredAd;

        [Binding]
        public uint ReservedSponsoredAd
        {
            get => _reservedSponsoredAd;
            set { Task.Run(delegate { ReservedSponsoredAdListAdapterOnItemSelected(_reservedSponsoredAd = value); }); }
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

        private async void ReservedSponsoredAdListAdapterOnItemSelected(uint index)
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


        [Binding]
        public void CreateAdButton_OnClick()
        {
            SwitchToView(nameof(CreateCompanyAdView));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}