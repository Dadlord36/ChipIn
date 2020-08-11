using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using DataModels;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using Repositories.Remote.Paginated;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    [Binding]
    public sealed class MerchantInterestViewModel : CorrespondingViewsSwitchingViewModel<MerchantInterestView>,
        INotifyPropertyChanged
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private MerchantInterestPagesPaginatedRepository merchantInterestPagesPaginatedRepository;
        [SerializeField] private MerchantInterestPagesListAdapter merchantInterestPagesListAdapter;


        private string _interestName;
        private uint _selectedInterestId;
        private Sprite _logoSprite;
        [SerializeField] private Sprite defaultLogo; //TODO: ViewsLogoController


        private readonly AsyncOperationCancellationController _asyncOperationCancellationController
            = new AsyncOperationCancellationController();

        private MarketInterestDetailsDataModel _selectedCommunityData;

        [Binding]
        public uint SelectedInterestId

        {
            get => _selectedInterestId;
            set
            {
                _selectedInterestId = value;
                OnInterestIdSelected();
            }
        }


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
            get
            {
                if (!_logoSprite)
                {
                    _logoSprite = defaultLogo;
                }

                return _logoSprite;
            }
            private set
            {
                _logoSprite = value;
                OnPropertyChanged();
            }
        }

        public MerchantInterestViewModel() : base(nameof(MerchantInterestViewModel))
        {
        }


        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                if (RelatedView.FormTransitionBundle.TransitionData == null) return;

                var selectedCommunityId = (int) (uint) RelatedView.FormTransitionBundle.TransitionData;

                merchantInterestPagesPaginatedRepository.SelectedCommunityId = selectedCommunityId;

                try
                {
                    await merchantInterestPagesListAdapter.Initialize().ConfigureAwait(true);
                }
                catch (OperationCanceledException)
                {
                    LogUtility.PrintDefaultOperationCancellationLog(Tag);
                }


                _selectedCommunityData = await GetSelectedCommunityDetailsAsync(selectedCommunityId).ConfigureAwait(true);

                InterestName = _selectedCommunityData.Name;
                await SetLogoFromUrlAsync(_selectedCommunityData.PosterUri).ConfigureAwait(true);
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

        private Task<MarketInterestDetailsDataModel> GetSelectedCommunityDetailsAsync(int selectedCommunityId)
        {
            return CommunitiesStaticRequestsProcessor.GetCommunityDetails(out _asyncOperationCancellationController
                    .TasksCancellationTokenSource, authorisationDataRepository, selectedCommunityId)
                .ContinueWith(task => task.GetAwaiter().GetResult().ResponseModelInterface.LabelDetailsDataModel,
                    TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private void OnInterestIdSelected()
        {
            SwitchToView(nameof(MerchantInterestDetailsView),
                new FormsTransitionBundle(new MerchantInterestDetailsViewModel.CommunityAndInterestIds(
                    (int) _selectedCommunityData.Id,
                    (int) _selectedInterestId)));
        }


        private Task SetLogoFromUrlAsync(in string url)
        {
            _asyncOperationCancellationController.CancelOngoingTask();
            return downloadedSpritesRepository
                .CreateLoadSpriteTask(url, _asyncOperationCancellationController.CancellationToken)
                .ContinueWith(
                    delegate(Task<Sprite> finishedTask) { LogoSprite = finishedTask.GetAwaiter().GetResult(); },
                    scheduler: DownloadedSpritesRepository.MainThreadScheduler,
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