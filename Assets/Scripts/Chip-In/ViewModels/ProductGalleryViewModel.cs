using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.InteractiveWindows;

namespace ViewModels
{
    [Binding]
    public sealed class ProductGalleryViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private const string Tag = nameof(ProductGalleryViewModel);
        public event PropertyChangedEventHandler PropertyChanged;

        #region SerielizedFields

        [SerializeField] private OffersRemoteRepository offersRemoteRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;
        [SerializeField] private GameIconsRepository gameIconsRepository;

        #endregion

        private ProductGalleryView ViewAsProductGalleryView => View as ProductGalleryView;

        private uint _selectedOfferPrice;
        private bool _offerIsSelected;

        private int? SelectedOfferId => ViewAsProductGalleryView.CurrentlySelectedOfferId;

        [Binding]
        public uint SelectedOfferPrice
        {
            get => _selectedOfferPrice;
            set
            {
                if (value == _selectedOfferPrice) return;
                _selectedOfferPrice = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool OfferIsSelected
        {
            get => _offerIsSelected;
            private set
            {
                if (value == _offerIsSelected) return;
                _offerIsSelected = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public async Task ConfirmItemSelection()
        {
            try
            {
                OfferIsSelected = SelectedOfferId != int.MinValue;
                var response = await OffersStaticRequestProcessor.GetOfferDetails(out TasksCancellationTokenSource, authorisationDataRepository,
                    SelectedOfferId);

                var offer = response.ResponseModelInterface.Offer;
                await InfoPanelView.FillWithData(ViewAsProductGalleryView, offer, offer, offer, offer);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public void ReturnToItemsSelectionState()
        {
            OfferIsSelected = false;
        }


        [Binding]
        public void HideOfferInfoCard()
        {
            ViewAsProductGalleryView.HideInfoCard();
        }

        [Binding]
        public async Task SubscribeToSelectedOfferGame()
        {
            try
            {
                var offerDetails = await OffersStaticRequestProcessor.GetOfferDetails(out TasksCancellationTokenSource, authorisationDataRepository,
                    SelectedOfferId).ConfigureAwait(false);

                var gameId = offerDetails.ResponseModelInterface.Offer.GameData.Id;
                var response = await UserGamesStaticProcessor.TryJoinAGame(out TasksCancellationTokenSource, authorisationDataRepository, gameId);
                if (response.Success)
                {
                    await gameIconsRepository.StoreNewGameIconsSet(gameId, response.ResponseModelInterface.GameBoard.Icons).ConfigureAwait(false);
                }
                else
                {
                    alertCardController.ShowAlertWithText(response.Error);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public void ShowSelectedOfferInfo()
        {
            ViewAsProductGalleryView.ShowInfoCard();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ViewAsProductGalleryView.NewCategorySelected += OnNewOffersCategorySelected;
            ViewAsProductGalleryView.RelatedItemSelected += OnOfferSelected;
            offersRemoteRepository.DataWasLoaded += OffersRemoteRepositoryOnCollectionChanged;
        }


        protected override void OnDisable()
        {
            base.OnDisable();
            ViewAsProductGalleryView.NewCategorySelected -= OnNewOffersCategorySelected;
            ViewAsProductGalleryView.RelatedItemSelected -= OnOfferSelected;
            offersRemoteRepository.DataWasLoaded -= OffersRemoteRepositoryOnCollectionChanged;
        }

        private void OffersRemoteRepositoryOnCollectionChanged()
        {
            FillDropdownListWithItemsOfCurrentCategory(ViewAsProductGalleryView.CurrentlySelectedOffersCategory);
        }

        private void OnOfferSelected(int offerId)
        {
            LogUtility.PrintLog(Tag, $"Selected offer Id is: {offerId.ToString()}");
        }

        private void OnNewOffersCategorySelected(string selectedOffersCategory)
        {
            FillDropdownListWithItemsOfCurrentCategory(selectedOffersCategory);
        }

        private void FillDropdownListWithItemsOfCurrentCategory(string selectedCategory)
        {
            var items = (from offerWithIdentifierData in offersRemoteRepository.ItemsData
                where string.Equals(selectedCategory, offerWithIdentifierData.Segment, StringComparison.OrdinalIgnoreCase)
                select new {offerWithIdentifierData.Id, offerWithIdentifierData.Title}).ToDictionary(arg => arg.Id, arg => arg.Title);

            if (!items.Any())
            {
                LogUtility.PrintLog(Tag, $"There is no items of offers category \"{selectedCategory}\"");
            }

            ViewAsProductGalleryView.FillDropdownList(items);
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await offersRemoteRepository.LoadDataFromServer();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public void SwitchToChallengesView()
        {
            SwitchToView(nameof(ChallengeView));
        }


        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}