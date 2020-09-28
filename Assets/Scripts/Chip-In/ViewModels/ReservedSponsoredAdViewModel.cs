using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels;
using DataModels.Interfaces;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using Repositories.Remote.Paginated;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class ReservedSponsoredAdViewModel : CorrespondingViewsSwitchingViewModel<ReservedSponsoredAdView>, INotifyPropertyChanged
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private SponsoredAdRepository reservedSponsoredAdRepository;

        private string _posterUri;
        private Sprite _backgroundPoster;
        private SponsoredAdDataModel _sponsoredAdData;

        [Binding]
        public Sprite BackgroundPoster
        {
            get => _backgroundPoster;
            private set
            {
                if (Equals(value, _backgroundPoster)) return;
                _backgroundPoster = value;
                OnPropertyChanged();
            }
        }

        public ReservedSponsoredAdViewModel() : base(nameof(ReservedSponsoredAdViewModel))
        {
        }

        [Binding]
        public void AdoptButton_OnClick()
        {
            SwitchToPreviousView();
        }

        [Binding]
        public void CancelReservationButton_OnClick()
        {
            /*reservedSponsoredAdRepository.RemoveItem(_sponsoredAdData);*/
            SwitchToPreviousView();
        }


        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            _sponsoredAdData = RelatedView.FormTransitionBundle.TransitionData as SponsoredAdDataModel;
            if (_sponsoredAdData == null)
            {
                LogUtility.PrintLogError(Tag, "Transition data is null");
                return;
            }

            try
            {
                _posterUri = ((IPosterImageUri) _sponsoredAdData).PosterUri;
                BackgroundPoster = await downloadedSpritesRepository.CreateLoadSpriteTask(_posterUri, OperationCancellationController.CancellationToken)
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}