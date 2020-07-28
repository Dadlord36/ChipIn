using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Controllers;
using DataModels;
using DataModels.Interfaces;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Temporary;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class SponsoredAdViewModel : CorrespondingViewsSwitchingViewModel<SponsoredAdView>, INotifyPropertyChanged
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private SponsoredAdRepository sponsoredAdRepository;

        private readonly AsyncOperationCancellationController _asyncOperationCancellationController
            = new AsyncOperationCancellationController();

        private string _posterUri;
        private Sprite _backgroundPoster;

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

        public SponsoredAdViewModel() : base(nameof(SponsoredAdViewModel))
        {
        }

        [Binding]
        public void CancelReservationButton_OnClick()
        {
            SwitchToPreviousView();
        }

        [Binding]
        public void AdoptButton_OnClick()
        {
            sponsoredAdRepository.AddItem(new SponsoredAdDataModel {PosterUri = _posterUri});
            SwitchToPreviousView();
        }


        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            var transitionData = RelatedView.FormTransitionBundle.TransitionData;

            try
            {
                _posterUri = ((IPosterImageUri) transitionData).PosterUri;
                BackgroundPoster = await downloadedSpritesRepository
                    .CreateLoadSpriteTask(_posterUri, _asyncOperationCancellationController.CancellationToken)
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}