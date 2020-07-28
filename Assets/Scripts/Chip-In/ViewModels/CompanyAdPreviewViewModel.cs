using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;
using WebOperationUtilities;

namespace ViewModels
{
    [Binding]
    public sealed class CompanyAdPreviewViewModel : CorrespondingViewsSwitchingViewModel<CompanyAdPreviewView>, INotifyPropertyChanged
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;

        private Sprite _backgroundSprite;
        private int _selectedItemIndex;
        private string _selectedFeatureDescription;

        private CompanyAdFeaturesPreviewData _companyAdFeaturesPreviewData;
        private bool _isSendingRequest;

        [Binding]
        public string SelectedFeatureDescription
        {
            get => _selectedFeatureDescription;
            set
            {
                if (value == _selectedFeatureDescription) return;
                _selectedFeatureDescription = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                if (value == _selectedItemIndex) return;
                _selectedItemIndex = value;
                SelectedFeatureDescription = _companyAdFeaturesPreviewData.FeatureModelsToPreview[value].Description;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool IsSendingRequest
        {
            get => _isSendingRequest;
            set
            {
                if (value == _isSendingRequest) return;
                _isSendingRequest = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite BackgroundSprite
        {
            get => _backgroundSprite;
            set
            {
                _backgroundSprite = value;
                OnPropertyChanged();
            }
        }

        public CompanyAdPreviewViewModel() : base(nameof(CompanyAdPreviewViewModel))
        {
        }

        [Binding]
        public async void ConfirmButton_OnClick()
        {
            try
            {
                IsSendingRequest = true;
                var result = await AdvertStaticRequestsProcessor.CreateAnAdvert(authorisationDataRepository, _companyAdFeaturesPreviewData)
                    .ConfigureAwait(true);
                alertCardController.ShowAlertWithText(result.IsSuccessful ? "Advert created successfully" : result.ErrorMessage);
                SwitchToView(nameof(ConnectView));
            }
            catch (OperationCanceledException)
            {
                alertCardController.ShowAlertWithText("Operation was cancelled");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            finally
            {
                IsSendingRequest = false;
            }
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            _companyAdFeaturesPreviewData = RelatedView.FormTransitionBundle.TransitionData as CompanyAdFeaturesPreviewData;
            Debug.Assert(_companyAdFeaturesPreviewData != null, nameof(_companyAdFeaturesPreviewData) + " != null");

            if (_companyAdFeaturesPreviewData.CompanyPosterImagePath != null)
                BackgroundSprite = SpritesUtility.CreateSpriteWithDefaultParameters(
                    await SpritesUtility.CreateTexture2DFromPathAsync(_companyAdFeaturesPreviewData.CompanyPosterImagePath, GameManager.MainThreadScheduler)
                        .ConfigureAwait(true)
                );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}