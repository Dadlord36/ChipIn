using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Temporary;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public sealed class SponsoredAdViewModel : CorrespondingViewsSwitchingViewModel<SponsoredAdView>, INotifyPropertyChanged
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private SponsoredAdRepository sponsoredAdRepository;
        [SerializeField] private SponsoredAdFullListAdapter sponsoredAdFullListAdapter;

        private uint _selectedItemIndex;
        
        [Binding]
        public uint SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                _selectedItemIndex = value;
                LogUtility.PrintLog(Tag, value.ToString());
            }
        }

        public SponsoredAdViewModel() : base(nameof(SponsoredAdViewModel))
        {
        }

        
        [Binding]
        public void AdoptButton_OnClick()
        {
            SwitchToPreviousView();
        }

        [Binding]
        public void ReserveButton_OnClick()
        {
            SwitchToPreviousView();
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            var transitionData = RelatedView.FormTransitionBundle.TransitionData;

            try
            {
                await sponsoredAdFullListAdapter.ResetAsync().ConfigureAwait(false);
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}