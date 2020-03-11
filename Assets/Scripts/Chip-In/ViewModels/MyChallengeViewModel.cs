using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Repositories.Local;
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
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private UserGamesRemoteRepository userGamesRemoteRepository;

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
                SelectedGameId = itemsList[0].Id;
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

        private async Task LoadGamesList()
        {
            await userGamesRemoteRepository.LoadDataFromServer();
        }

        private async Task LoadDataAndFillTheList()
        {
            await LoadGamesList();
            FillDropdownList();
        }

        private void FillDropdownList()
        {
            var itemsList = userGamesRemoteRepository.ItemsData;

            var itemsNamesDictionary = new Dictionary<int, string>(itemsList.Count);
            for (int i = 0; i < itemsList.Count; i++)
            {
                var gameId = itemsList[i].Id;
                itemsNamesDictionary.Add(gameId, gameId.ToString());
            }

            FillDropdownList(itemsNamesDictionary);
        }

        private void FillDropdownList(Dictionary<int, string> dictionary)
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