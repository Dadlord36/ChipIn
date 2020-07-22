using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public sealed class ConnectViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private CompanyAdListAdapter companyAdListAdapter;
        [SerializeField] private SponsoredAdListAdapter sponsoredAdListAdapter;
        [SerializeField] private SponsoredAdListAdapter reservedSponsoredAdListAdapter;


        private bool _companyAdListIsNotEmpty;



        public ConnectViewModel() : base(nameof(ConnectViewModel))
        {
        }


        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
               await Task.WhenAll(companyAdListAdapter.Initialize(), sponsoredAdListAdapter.Initialize(), reservedSponsoredAdListAdapter.Initialize())
               .ConfigureAwait(true);
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

        [Binding]
        public void CreateAdButton_OnClick()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}