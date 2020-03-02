using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Repositories.Local;
using UnityEngine;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class MyChallengeViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private UserGamesRemoteRepository userGamesRemoteRepository;
        private int _selectedElementIndexInGamesList;
        private bool _challengeIsSelected;


        private MyChallengeView ThisView => (MyChallengeView) View;

        private int SelectedElementIndexInGamesList
        {
            set
            {
                _selectedElementIndexInGamesList = value;
                selectedGameRepository.SelectedElementIndexInGamesList = value;
                SelectedGameIndex = userGamesRemoteRepository[_selectedElementIndexInGamesList].Id;
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

        private int SelectedGameIndex
        {
            get => selectedGameRepository.GameId;
            set => selectedGameRepository.GameId = value;
        }

        [Binding]
        public void ShowInfo_OnButtonClick()
        {
            throw new NotImplementedException();
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
            ThisView.SelectedItemIndexChanged += OnSelectedItemIndexChanged;
            ThisView.ItemsListUpdated += OnItemsListUpdated;
        }

        private void UnsubscribeFromViewEvents()
        {
            ThisView.SelectedItemIndexChanged -= OnSelectedItemIndexChanged;
            ThisView.ItemsListUpdated -= OnItemsListUpdated;
        }

        private void OnItemsListUpdated()
        {
            SelectedElementIndexInGamesList = 0;
        }

        private void OnSelectedItemIndexChanged(int newIndex)
        {
            SelectedElementIndexInGamesList = newIndex;
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
            var itemsNamesList = new string[itemsList.Count];

            for (int i = 0; i < itemsList.Count; i++)
            {
                itemsNamesList[i] = itemsList[i].Id.ToString();
            }

            FillDropdownList(itemsNamesList);
        }

        private void FillDropdownList(string[] itemsList)
        {
            var myChallengeView = View as MyChallengeView;
            Debug.Assert(myChallengeView != null, nameof(myChallengeView) + " != null");
            myChallengeView.FillDropdownList(itemsList);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}