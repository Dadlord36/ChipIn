using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using Repositories.Remote.Paginated;
using RequestsStaticProcessors;
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
    public sealed class MerchantInterestViewModel : CorrespondingViewsSwitchingViewModel<MerchantInterestView>, INotifyPropertyChanged
    {
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private MerchantInterestPagesPaginatedRepository merchantInterestPagesPaginatedRepository;
        [SerializeField] private MerchantInterestPagesListAdapter merchantInterestPagesListAdapter;
        [SerializeField] private Sprite defaultLogo; //TODO: ViewsLogoController


        private string _interestName;
        private int _selectedInterestId;
        private Sprite _logoSprite;
        private bool _hasItemsToShow = true;

        private MarketInterestDetailsDataModel _selectedCommunityData;

        [Binding]
        public int SelectedInterestId
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
        public bool HasItemsToShow
        {
            get => _hasItemsToShow;
            set
            {
                if (value == _hasItemsToShow) return;
                _hasItemsToShow = value;
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
                if (RelatedView.FormTransitionBundle.TransitionData == null)
                {
                    LogUtility.PrintLog(Tag, "<color=red>TransitionData is NULL</color>");
                    return;
                }

                var selectedCommunityId = (int) RelatedView.FormTransitionBundle.TransitionData;
                LogUtility.PrintLog(Tag, $"<color=blue>{nameof(selectedCommunityId)} is {selectedCommunityId.ToString()}</color>");

                // merchantInterestPagesPaginatedRepository.SelectedCommunityId should be set first before requesting  merchantInterestPagesListAdapter ResetAsync
                // so that requested data will be related to the selected previously CommunityId
                {
                    merchantInterestPagesPaginatedRepository.SelectedCommunityId = selectedCommunityId;
                    await merchantInterestPagesListAdapter.ResetAsync().ConfigureAwait(false);
                }

                {
                    _selectedCommunityData = await GetSelectedCommunityDetailsAsync(selectedCommunityId)
                        .ConfigureAwait(false);

                    InterestName = _selectedCommunityData.Name;

                    LogoSprite = await downloadedSpritesRepository.CreateLoadSpriteTask(_selectedCommunityData.PosterUri,
                        OperationCancellationController.CancellationToken)
                        .ConfigureAwait(false);
                }
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

        /*public async void FundTheInterest(int tokensAmount)
        {
            if (tokensAmount <= 0) return;

            try
            {
                var response = await CommunitiesInterestsStaticProcessor.FundInterest(out AsyncOperationCancellationController.TasksCancellationTokenSource,
                    authorisationDataRepository, (int) InterestId, tokensAmount);

                alertCardController.ShowAlertWithText(response.Success ? "Successfully fund this interest" : response.Error);
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
        }*/

        private async Task<MarketInterestDetailsDataModel> GetSelectedCommunityDetailsAsync(int selectedCommunityId)
        {
            var response = await CommunitiesStaticRequestsProcessor.GetCommunityDetails(out OperationCancellationController.TasksCancellationTokenSource,
                authorisationDataRepository, selectedCommunityId).ConfigureAwait(false);
            return response.ResponseModelInterface.LabelDetailsDataModel;
        }

        private void OnInterestIdSelected()
        {
            SwitchToView(nameof(MerchantInterestDetailsView),
                new FormsTransitionBundle(new MerchantInterestDetailsViewModel.CommunityAndInterestIds((int) _selectedCommunityData.Id,
                    _selectedInterestId)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}