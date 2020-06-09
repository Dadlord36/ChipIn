using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Controllers;
using DataModels.Interfaces;
using DataModels.MatchModels;
using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using Utilities;
using ViewModels;
using WebSockets;

namespace Behaviours.Games
{
    public class SlotsGameBehaviour : AsyncOperationsMonoBehaviour
    {
        private const string Tag = nameof(SlotsGameBehaviour);

        #region Public structs declaration

        public struct SlotGameRoundData
        {
            public int Number { get; private set; }
            public float RoundEndsInSeconds { get; private set; }
            public int? WinnerId { get; private set; }

            private SlotsBoardData _slotsBoardData;
            public ISlotIconBaseData[] SlotsIconsData => _slotsBoardData.GetIconsData();
            public MatchUserDownloadingData[] UsersData { get; private set; }
            public int[] IdenticalActiveElementsIndexes => _slotsBoardData.GetIdenticalActiveSlotsIndexes();

            public void SetSlotsBoardData(SlotsBoardData boardDataData)
            {
                _slotsBoardData = boardDataData;
#if UseLOG
                LogBoardIndexes(_slotsBoardData);
#endif
            }

            public void Update(MatchStateData matchStateData)
            {
                Update(matchStateData.MatchState.Body, matchStateData.MatchState.Round);
            }

            public void Update(IMatchModel matchModel)
            {
                Update(matchModel, matchModel.RoundNumber);
            }

            private void Update(IBaseMatchModel matchDataModel, uint roundNumber)
            {
                SetSlotsBoardData(matchDataModel.BoardData);
                UsersData = matchDataModel.Users;
                WinnerId = matchDataModel.WinnerId;
                RoundEndsInSeconds = matchDataModel.RoundEndsAt;
                Number = (int) roundNumber;
            }

            private static void LogBoardIndexes(SlotsBoardData slotsBoardData)
            {
                var iconsData = slotsBoardData.GetIconsData();
                var stringBuilder = new StringBuilder(iconsData[0].IconId.ToString());
                for (int i = 1; i < iconsData.Length; i++)
                {
                    stringBuilder.Append($" : {iconsData[i].IconId.ToString()}");
                }

                LogUtility.PrintLog(Tag, $"New indexes: {stringBuilder}");
            }
        }

        #endregion


        #region Private serialized fields

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private SlotsGameViewModel gameInterfaceViewModel;
        [SerializeField] private AlertCardController alertCardController;
        
        #endregion

        private ISlotsGame GameInterface => gameInterfaceViewModel;

        private GameChannelWebSocketSharp _gameChannelSocket;
        private bool _gameShouldBeFinished;
        private bool _roundEnds;
        private SlotGameRoundData _roundData;


        private int RoundNumber
        {
            get => GameInterface.RoundNumber;
            set => GameInterface.RoundNumber = value;
        }

        public void Activate()
        {
            GetGameDataAndInitializeGame();
            SubscribeToGameViewEvents();
        }

        public void Deactivate()
        {
            CloseConnectionAndDisposeGameChannelSocket();
            UnsubscribeFromGameViewEvents();
        }

        private void SubscribeToGameViewEvents()
        {
            GameInterface.SpinBoardRequested += SpinBoard;
            GameInterface.SpinFrameRequested += SpinFrame;
        }

        private void UnsubscribeFromGameViewEvents()
        {
            GameInterface.SpinBoardRequested -= SpinBoard;
            GameInterface.SpinFrameRequested -= SpinFrame;
        }

        private async Task GetGameDataAndInitializeGame()
        {
            GameInterface.Initialize();
            try
            {
                await InitializeMatch();
                await UpdateGameRepositoryUsersData();
                await StartGameSocketChannel();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            StartNewRound();
        }

        private async Task InitializeMatch()
        {
            try
            {
                var response = await UserGamesStaticProcessor.TryShowMatch(out TasksCancellationTokenSource, authorisationDataRepository,
                    selectedGameRepository.GameId);

                if (!response.Success || !response.ResponseModelInterface.Success)
                {
                    alertCardController.ShowAlertWithText(response.Error);
                    return;
                }

                var matchData = response.ResponseModelInterface;
                var roundTime = matchData.MatchData.RoundEndsAt;
                var roundNumber = matchData.MatchData.RoundNumber;
                var timeForPassedRounds = (int) (roundNumber * roundTime);

                var timeSpanFromGameStarted = DateTime.Now - selectedGameRepository.SelectedGameData.StartedAt;
                var secondsSinsRoundHaveStarted = timeForPassedRounds - timeSpanFromGameStarted.Seconds;

                LogUtility.PrintLog(Tag, $"Seconds sins round has started: {secondsSinsRoundHaveStarted.ToString()}");
                matchData.MatchData.RoundEndsAt = secondsSinsRoundHaveStarted;

                _roundData.Update(matchData.MatchData);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        private void StartNewRound()
        {
            GameInterface.RefillIconsSet(_roundData.SlotsIconsData);

            LogUtility.PrintLog(Tag, "Round has started");
            RoundNumber = _roundData.Number;
            UpdateSlotsIconsPositionsAndActivity();
            GameInterface.StartTimer(_roundData.RoundEndsInSeconds);
            GameInterface.AllowInteractivity();
        }

        private void FinishTheGame()
        {
            _gameShouldBeFinished = false;
            PrintLog("Game is over");
            CloseConnectionAndDisposeGameChannelSocket();
            GameInterface.OnMatchEnds();
        }

        private void Update()
        {
            if (_roundEnds)
            {
                _roundEnds = false;
                UpdateUsersData();
                StartNewRound();
            }

            if (_gameShouldBeFinished)
                FinishTheGame();
        }


        private async Task UpdateGameRepositoryUsersData()
        {
            try
            {
                await selectedGameRepository.SaveGameSateData(_roundData);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void CloseConnectionAndDisposeGameChannelSocket()
        {
            if (_gameChannelSocket == null) return;

            _gameChannelSocket.Close();
            (_gameChannelSocket as IDisposable).Dispose();
        }


        private async Task StartGameSocketChannel()
        {
            CloseConnectionAndDisposeGameChannelSocket();
            try
            {
                await EstablishSocketConnection();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            SubscribeToGameSocketEvents();
        }

        private void SubscribeToGameSocketEvents()
        {
            _gameChannelSocket.RoundEnds += GameChannelSocketOnMatchRoundEnds;
            _gameChannelSocket.MatchEnds += GameChannelSocketOnMatchEnds;
        }

        private async Task EstablishSocketConnection()
        {
            try
            {
                _gameChannelSocket = new GameChannelWebSocketSharp(authorisationDataRepository.GetRequestHeaders());
                await Task.Run(delegate { _gameChannelSocket.ConnectAsync(); });
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        private void UpdateUsersData()
        {
            selectedGameRepository.UpdateUsersData(_roundData.UsersData);
        }


        private void GameChannelSocketOnMatchRoundEnds(MatchStateData matchStateData)
        {
            UdataRoundData(matchStateData);
        }

        private void GameChannelSocketOnMatchEnds(MatchStateData matchStateData)
        {
            selectedGameRepository.WinnerId = matchStateData.MatchState.Body.WinnerId;
            _gameShouldBeFinished = true;
        }

        private void UdataRoundData(MatchStateData matchStateData)
        {
            _roundData.Update(matchStateData);
            _roundEnds = true;
        }

        private void SpinFrame()
        {
            MakeASpin(SpinBoardParameters.JustFrame);
        }

        private void SpinBoard()
        {
            MakeASpin(SpinBoardParameters.JustBoard);
        }

        #region Functions, dependent from RoundData

        #endregion


        private async Task MakeASpin(SpinBoardParameters spinBoardParameters)
        {
            try
            {
                var result = await UserGamesStaticProcessor.TryMakeAMove(out TasksCancellationTokenSource, authorisationDataRepository,
                    selectedGameRepository.GameId, spinBoardParameters);

                _roundData.SetSlotsBoardData(result.ResponseModelInterface.BoardData);

                UpdateSlotsIconsPositionsAndActivity(spinBoardParameters);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void AnimateMatchingSlots()
        {
            GameInterface.StopAnimatingSlots();

            var identicalItemsIndexes = _roundData.IdenticalActiveElementsIndexes;
            if (identicalItemsIndexes == null || identicalItemsIndexes.Length <= 1) return;
            var matchingNumber = identicalItemsIndexes.Length;

            GameInterface.AnimateSlotsById(identicalItemsIndexes);
            PrintLog($"{matchingNumber.ToString()} items are matching!");
        }

        private void UpdateSlotsIconsPositionsAndActivity()
        {
            GameInterface.SwitchIconsInSlots(_roundData.SlotsIconsData);
            AnimateMatchingSlots();
            PrintLog("Slots Icons was updated");
        }

        private void UpdateSlotsIconsPositionsAndActivity(in SpinBoardParameters spinBoardParameters)
        {
            GameInterface.SetSpinTargetsAndStartSpinning(_roundData.SlotsIconsData, spinBoardParameters);
            AnimateMatchingSlots();
            PrintLog("Slots Icons was updated");
        }

        private static void PrintLog(string message)
        {
            LogUtility.PrintLog(Tag, message);
        }
    }
}