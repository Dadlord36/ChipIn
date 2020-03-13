﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels.Interfaces;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.InteractiveWindows;

namespace ViewModels
{
    [Binding]
    public sealed class MyChallengeViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private const string Tag = nameof(MyChallengeView);

        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private UserGamesRemoteRepository userGamesRemoteRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        private bool _challengeIsSelected;


        private MyChallengeView ThisView => (MyChallengeView) View;

        private int SelectedGameId
        {
            get => selectedGameRepository.GameId;
            set
            {
                if (value == selectedGameRepository.GameId) return;
                selectedGameRepository.GameId = value;
                ChallengeIsSelected = true;
                OnPropertyChanged();
            }
        }


        [Binding]
        public bool ChallengeIsSelected
        {
            get => _challengeIsSelected;
            private set
            {
                if (value == _challengeIsSelected) return;
                _challengeIsSelected = value;
                LogUtility.PrintLog(Tag, $"ChallengeIsSelected: {_challengeIsSelected.ToString()}");
                OnPropertyChanged();
            }
        }


        [Binding]
        public async Task ShowInfo_OnButtonClick()
        {
            try
            {
                await FillInfoCardWithGameRelatedOfferData(SelectedGameId);
                ThisView.ShowInfoCard();
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

        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeToViewEvents();
            LoadDataAndFillTheList();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromViewEvents();
        }
        
        private void SubscribeToViewEvents()
        {
            ThisView.RelatedItemSelected += OnSelectedItemIndexChanged;
            ThisView.ItemsListUpdated += OnItemsListUpdated;
        }

        private void UnsubscribeFromViewEvents()
        {
            ThisView.RelatedItemSelected -= OnSelectedItemIndexChanged;
            ThisView.ItemsListUpdated -= OnItemsListUpdated;
        }

        private void OnItemsListUpdated()
        {
            var itemsList = userGamesRemoteRepository.ItemsData;
            //Set first game id as selected in list
            if (itemsList.Count > 0)
            {
                SelectedGameId = itemsList[0].Id;
                ChallengeIsSelected = true;
            }
        }

        private void OnSelectedItemIndexChanged(int newId)
        {
            SelectedGameId = newId;
        }

        private async Task FillInfoCardWithGameRelatedOfferData(int selectedGameId)
        {
            var responseModel = await userGamesRemoteRepository.GetOfferDataForGivenGameId(selectedGameId);
            await InfoPanelView.FillWithData(ThisView, responseModel.Offer);
        }

        private Task LoadGamesList()
        {
            return userGamesRemoteRepository.LoadDataFromServer();
        }

        private async void LoadDataAndFillTheList()
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

        private async Task FillDropdownList()
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

        private void FillDropdownList(Dictionary<int?, string> dictionary)
        {
            var myChallengeView = View as MyChallengeView;
            Debug.Assert(myChallengeView != null, nameof(myChallengeView) + " != null");
            myChallengeView.FillDropdownList(dictionary);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}