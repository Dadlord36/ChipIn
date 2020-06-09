using System;
using System.ComponentModel;
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
using Views.ViewElements;

namespace ViewModels
{
    [Binding]
    public sealed class ChallengeViewModel : BaseContainerItemsViewModel, INotifyPropertyChanged
    {
        [SerializeField] private ChallengesCardsParametersRepository challengesCardsParametersRepository;
        [SerializeField] private ChallengesRemoteRepository challengesRemoteRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private AlertCardController alertCardController;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private Timer timer;
        private bool _canStartTheGame;

        private ChallengeView ChallengeView => View as ChallengeView;

        [Binding]
        public bool CanStartTheGame
        {
            get => _canStartTheGame;
            set
            {
                if (value == _canStartTheGame) return;
                _canStartTheGame = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public async Task Play_OnButtonClick()
        {
            try
            {
                if (await CanGameStarts())
                    SwitchToSlotsGameView();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);;
                throw;
            }
        }

        private async Task<bool> CanGameStarts()
        {
            try
            {
                var response = await UserGamesStaticProcessor.TryShowMatch(out TasksCancellationTokenSource,authorisationDataRepository,
                    selectedGameRepository.GameId);
                if (response.Success && response.ResponseModelInterface.Success) return true;
                alertCardController.ShowAlertWithText(response.Error);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            return false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            InitializeComponents();
            ResetComponents();

            SubscribeOnEvents();
            CheckIfGameCanBePlayed();
        }

        private void ResetComponents()
        {
            CanStartTheGame = false;
        }

        private void InitializeComponents()
        {
            timer.Initialize();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvent();
        }

        private void SubscribeOnEvents()
        {
            SubscribeOnRepositoryItemsCollectionChangesEvent(challengesRemoteRepository);
            timer.OnElapsed += AllowToPlay;
        }

        private void UnsubscribeFromEvent()
        {
            UnsubscribeOnRepositoryItemsCollectionChangesEvent(challengesRemoteRepository);
            timer.OnElapsed -= AllowToPlay;
        }

        private void SwitchToSlotsGameView()
        {
            SwitchToView(nameof(SlotsGameView));
        }

        private void AddCard(string challengeTypeName, uint coinsAmount)
        {
            var visibleParameters = challengesCardsParametersRepository.GetItemVisibleParameters(challengeTypeName);
            visibleParameters.coinsAmount = coinsAmount;
            var card = ((ChallengeView) View).AddItem();
            card.SetupCardViewElements(visibleParameters);
        }

        protected override void ClearAllItems()
        {
            // ((ChallengeView) View).RemoveAllItems();
        }

        private void CheckIfGameCanBePlayed()
        {
            if (selectedGameRepository.GameHasStarted)
            {
                AllowToPlay();
            }
            else
            {
                AllowToPlayOnTimer();
            }
        }

        private void AllowToPlay()
        {
            CanStartTheGame = true;
        }

        private void AllowToPlayOnTimer()
        {
            StartTimerCountdown(Mathf.Abs((float) selectedGameRepository.TimeTillGameStarts.TotalSeconds) + 1f);
        }

        private void StartTimerCountdown(float intervalInSeconds)
        {
            timer.SetAndStartTimer(intervalInSeconds);
        }

        protected override void FillContainerWithDataFromRepository()
        {
            foreach (var item in challengesRemoteRepository.ItemsData)
            {
                AddCard(item.ChallengeTypeName, item.CoinsPrice);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}