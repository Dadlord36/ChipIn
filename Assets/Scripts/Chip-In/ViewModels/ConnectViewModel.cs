using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using JetBrains.Annotations;
using Repositories.Remote.Paginated;
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

        public ConnectViewModel() : base(nameof(ConnectViewModel))
        {
        }


        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await Task.WhenAll(companyAdListAdapter.Initialize(), sponsoredAdListAdapter.Initialize(), 
                        reservedSponsoredAdListAdapter.Initialize())
                    .ConfigureAwait(true);
                sponsoredAdListAdapter.ItemSelected += ReservedSponsoredAdListAdapterOnItemSelected;
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

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            sponsoredAdListAdapter.ItemSelected -= ReservedSponsoredAdListAdapterOnItemSelected;
        }

        private async void ReservedSponsoredAdListAdapterOnItemSelected(uint index)
        {
            try
            {
                SponsoredAdDataModel data = await sponsoredAdRepository.CreateGetItemWithIndexTask(index).ConfigureAwait(true);
                
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