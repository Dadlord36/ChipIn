﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels.Interfaces;
using HttpRequests.RequestsProcessors.GetRequests;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;

namespace ViewModels
{
    [Binding]
    public class ProductGalleryViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private const string Tag = nameof(ProductGalleryViewModel);
        public event PropertyChangedEventHandler PropertyChanged;

        #region SerielizedFields

        [SerializeField] private OffersRemoteRepository offersRemoteRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        #endregion

        private ProductGalleryView ViewAsProductGalleryView => View as ProductGalleryView;

        private uint _selectedOfferPrice;
        private bool _offerIsSelected;

        private int SelectedOfferId => ViewAsProductGalleryView.CurrentlySelectedOfferId;

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
        public void ConfirmItemSelection()
        {
            OfferIsSelected = SelectedOfferId != int.MinValue;
        }

        [Binding]
        public async Task SubscribeToSelectedOfferGame()
        {
            var offerDetails = await GetOfferDetails(SelectedOfferId);
            var gameId = offerDetails.Offer.GameData.Id;
            var response = await UserGamesStaticProcessor.TryJoinAGame(authorisationDataRepository, gameId);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ViewAsProductGalleryView.NewCategorySelected += OnNewOffersCategorySelected;
            ViewAsProductGalleryView.RelatedItemSelected += OnOfferSelected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ViewAsProductGalleryView.NewCategorySelected -= OnNewOffersCategorySelected;
            ViewAsProductGalleryView.RelatedItemSelected -= OnOfferSelected;
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

        private Task<IOfferDetailsResponseModel> GetOfferDetails(int offerId)
        {
            return OffersStaticRequestProcessor.TryGetOfferDetails(new DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters(authorisationDataRepository, offerId));
        }


        [Binding]
        public void SwitchToChallengesView()
        {
            SwitchToView(nameof(ChallengeView));
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}