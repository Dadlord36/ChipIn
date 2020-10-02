using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels.Interfaces;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;
using Views.Bars.BarItems;
using Views.ViewElements.ScrollViews.Adapters;

namespace ViewModels
{
    //For testing purposes only
    /*[Serializable]
    public class CompanyAdFeatureDataModel : ICompanyAdFeatureModel
    {
        [SerializeField] private int tokensRewardAmount;
        [SerializeField] private string description;
        [SerializeField] private string posterImagePath;

        public CompanyAdFeatureDataModel(int tokensRewardAmount, string description, string posterImagePath)
        {
            this.tokensRewardAmount = tokensRewardAmount;
            this.description = description;
            this.posterImagePath = posterImagePath;
        }

        public int TokensRewardAmount
        {
            get => tokensRewardAmount;
            set => tokensRewardAmount = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string PosterImagePath
        {
            get => posterImagePath;
            set => posterImagePath = value;
        }
    }*/

    [Binding]
    public sealed class CompanyAdPreviewViewModel : CorrespondingViewsSwitchingViewModel<CompanyAdPreviewView>, INotifyPropertyChanged
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;

        [SerializeField] private DesignedListViewAdapter featuresListViewAdapter;
        [SerializeField] private bool listIconsAreLocalPaths = true;

        //For testing purposes only
        /*[SerializeField] private CompanyAdFeatureDataModel[] dataItems;
        [SerializeField] private string backgroundImagePath;*/


        private Sprite _backgroundSprite;
        private uint _selectedItemIndex;
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
        public uint SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                _selectedItemIndex = value;
                SelectedFeatureDescription = GetItemDescription((int) value);
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

        private string GetItemDescription(int index)
        {
            return _companyAdFeaturesPreviewData.FeatureModelsToPreview[index].Description;
        }
        //For testing purposes only
        /*private string GetItemDescription(uint index)
        {
            return dataItems[index].Description;
        }*/

        [Binding]
        public async void ConfirmButton_OnClick()
        {
            try
            {
                IsSendingRequest = true;
                IsAwaitingProcess = true;
                var result = await AdvertStaticRequestsProcessor.CreateAnAdvert(authorisationDataRepository, _companyAdFeaturesPreviewData)
                    .ConfigureAwait(false);
                alertCardController.ShowAlertWithText(result.IsSuccessful
                    ? "Advert created successfully"
                    : ErrorsHandlingUtility.CollectErrors(result.Content));
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
                IsAwaitingProcess = false;
                IsSendingRequest = false;
            }
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            OperationCancellationController.CancelOngoingTask();

            _companyAdFeaturesPreviewData = RelatedView.FormTransitionBundle.TransitionData as CompanyAdFeaturesPreviewData;
            try
            {
                Debug.Assert(_companyAdFeaturesPreviewData != null, nameof(_companyAdFeaturesPreviewData) + " != null");
                if (_companyAdFeaturesPreviewData.CompanyPosterImagePath != null)
                {
                    BackgroundSprite = await downloadedSpritesRepository.CreateLoadSpriteTask(_companyAdFeaturesPreviewData.CompanyPosterImagePath,
                            OperationCancellationController.CancellationToken, listIconsAreLocalPaths)
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

            RefillList(_companyAdFeaturesPreviewData.FeatureModelsToPreview);
        }

        //For testing purposes only
        /*private async void Awake()
        {
            try
            {
                featuresListViewAdapter.Initialized += () => { RefillList(dataItems); };
                BackgroundSprite = await downloadedSpritesRepository.CreateLoadSpriteTask(backgroundImagePath,
                        OperationCancellationController.CancellationToken, true)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }*/

        private void RefillList(IReadOnlyList<IAdvertFeatureBaseModel> featureModelsToPreview)
        {
            var itemsNumber = featureModelsToPreview.Count;
            var items = new List<DesignedScrollBarItemDefaultDataModel>(itemsNumber);

            for (var index = 0; index < itemsNumber; index++)
            {
                var dataModel = featureModelsToPreview[index];
                var loadIconTask = downloadedSpritesRepository.CreateLoadSpriteTask(dataModel.Icon,
                    OperationCancellationController.CancellationToken, listIconsAreLocalPaths);
                items.Add(new DesignedScrollBarItemDefaultDataModel(loadIconTask, (uint) index));
            }

            TasksFactories.ExecuteOnMainThread(() =>
            {
                if (featuresListViewAdapter.IsInitialized)
                {
                    featuresListViewAdapter.SetItems(items);
                }
                else
                {
                    void Refill()
                    {
                        featuresListViewAdapter.Initialized -= Refill;
                        featuresListViewAdapter.SetItems(items);
                    }

                    featuresListViewAdapter.Initialized += Refill;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}