using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using DataModels;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Local.SingleItem;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class MerchantInterestViewModel : CorrespondingViewsSwitchingViewModel<MerchantInterestView>, INotifyPropertyChanged
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private OfferCreationRepository offerCreationRepository;
        [SerializeField] private SelectedMerchantInterestRepository selectedMerchantInterestRepository;
        [SerializeField] private SelectedMerchantInterestPageRepository selectedMerchantInterestPageRepository;


        private string _interestName;
        private Sprite _logoSprite;
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();


        [Binding]
        public string InterestName
        {
            get => _interestName;
            private set
            {
                _interestName = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite LogoSprite
        {
            get => _logoSprite;
            private set
            {
                _logoSprite = value;
                OnPropertyChanged();
            }
        }

        public MerchantInterestViewModel() : base(nameof(MerchantInterestViewModel))
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnEvents();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvents();
        }


        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await SetViewNameToInterestName();
                await LoadAndSetInterestPageLogo();
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

        private void SubscribeOnEvents()
        {
            RelatedView.ItemSelected += RelatedViewOnItemSelected;
        }

        private void UnsubscribeFromEvents()
        {
            RelatedView.ItemSelected -= RelatedViewOnItemSelected;
        }

        private async void RelatedViewOnItemSelected(uint index)
        {
            try
            {
                selectedMerchantInterestPageRepository.SelectedInterestPageRepositoryIndex = index;
                await SaveSelectedInterestPageSegment();
                SwitchToView(nameof(MerchantInterestDetailsView));
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private Task SaveSelectedInterestPageSegment()
        {
            return selectedMerchantInterestPageRepository.CreateGetSelectedInterestPageDataTask().ContinueWith(
                delegate(Task<MerchantInterestPageDataModel> getDataTask)
                {
                    offerCreationRepository.OfferSegmentName = getDataTask.Result.Segment;
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private Task SetViewNameToInterestName()
        {
            return selectedMerchantInterestRepository.CreateGetSelectedInterestDataTask().ContinueWith(
                delegate(Task<MarketInterestDetailsDataModel> task) { InterestName = task.Result.Name; },
                scheduler: downloadedSpritesRepository.MainThreadScheduler,
                continuationOptions: TaskContinuationOptions.OnlyOnRanToCompletion,
                cancellationToken: _asyncOperationCancellationController.CancellationToken);
        }

        private Task LoadAndSetInterestPageLogo()
        {
            return selectedMerchantInterestRepository.CreateGetSelectedInterestDataTask().ContinueWith(dataLoadingTask
                => SetLogoFromUrl(dataLoadingTask.Result.PosterUri), TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap();
        }

        private Task SetLogoFromUrl(in string url)
        {
            _asyncOperationCancellationController.CancelOngoingTask();
            return downloadedSpritesRepository.CreateLoadSpriteTask(url, _asyncOperationCancellationController.CancellationToken)
                .ContinueWith(delegate(Task<Sprite> finishedTask) { LogoSprite = finishedTask.Result; },
                    scheduler: downloadedSpritesRepository.MainThreadScheduler,
                    continuationOptions: TaskContinuationOptions.OnlyOnRanToCompletion,
                    cancellationToken: _asyncOperationCancellationController.CancellationToken);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}