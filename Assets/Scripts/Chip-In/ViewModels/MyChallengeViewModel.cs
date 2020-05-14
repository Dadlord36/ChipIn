using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.Interfaces;
using Repositories.Local;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;
using Views.InteractiveWindows;

namespace ViewModels
{
    [Binding]
    public sealed class MyChallengeViewModel : BaseItemsListViewModel<MyChallengeView>
    {
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private UserGamesRemoteRepository userGamesRemoteRepository;


        private int SelectedGameId
        {
            get => selectedGameRepository.GameId;
            set
            {
                if (value == selectedGameRepository.GameId) return;
                selectedGameRepository.GameId = value;
                ItemIsSelected = true;
                OnPropertyChanged();
            }
        }
        

        [Binding]
        public async Task ShowInfo_OnButtonClick()
        {
            try
            {
                await FillInfoCardWithRelatedData(SelectedGameId);
                RelatedView.ShowInfoCard();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public void Challenge_OnButtonClick()
        {
            SwitchToChallengeView();
        }

        private void SwitchToChallengeView()
        {
            SwitchToView(nameof(ChallengeView));
        }

        protected override void OnItemsListUpdated()
        {
            var itemsList = userGamesRemoteRepository.ItemsData;
            //Set first game id as selected in list
            if (itemsList.Count > 0)
            {
                SelectedGameId = itemsList[0].Id;
                ItemIsSelected = true;
            }
        }

        protected override void OnSelectedItemIndexChanged(int relatedItemIndex)
        {
            base.OnSelectedItemIndexChanged(relatedItemIndex);
            SelectedGameId = relatedItemIndex;
        }

        protected override async Task FillInfoCardWithRelatedData(int selectedId)
        {
            var responseModel = await userGamesRemoteRepository.GetOfferDataForGivenGameId(selectedId);
            var offer = responseModel.Offer;
            await InfoPanelView.FillWithData(RelatedView, offer, offer, offer, offer);
        }

        private Task LoadGamesList()
        {
            return userGamesRemoteRepository.LoadDataFromServer();
        }

        protected override async void LoadDataAndFillTheList()
        {
            try
            {
                await LoadGamesList();
                await FillDropdownList();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        protected override async Task FillDropdownList()
        {
            var itemsList = userGamesRemoteRepository.ItemsData;

            var tasks = new List<Task<IOfferDetailsResponseModel>>(itemsList.Count);

            for (int i = 0; i < itemsList.Count; i++)
            {
                tasks.Add(userGamesRemoteRepository.GetOfferDataForGivenGameId(itemsList[i].Id));
            }

            var correspondingOffers = await Task.WhenAll(tasks);

            var itemsNamesDictionary = new Dictionary<int?, string>(itemsList.Count);
            for (int i = 0; i < itemsList.Count; i++)
            {
                itemsNamesDictionary.Add(itemsList[i].Id, correspondingOffers[i].Offer.Title);
            }

            FillDropdownList(itemsNamesDictionary);
        }
    }
}