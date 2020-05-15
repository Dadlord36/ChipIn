using System;
using System.Text;
using System.Threading.Tasks;
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
    public class SlotsGameBehaviour : MonoBehaviour
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

        private async void GetGameDataAndInitializeGame()
        {
            GameInterface.Initialize();
            await InitializeMatch();
            await UpdateGameRepositoryUsersData();
            await StartGameSocketChannel();
            StartNewRound();
        }

        private async Task InitializeMatch()
        {
            try
            {
                var response = await UserGamesStaticProcessor.TryShowMatch(authorisationDataRepository,
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

                GameInterface.RefillIconsSet();

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
            await selectedGameRepository.SaveGameSateData(_roundData);
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
            await EstablishSocketConnection();
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


        private async void MakeASpin(SpinBoardParameters spinBoardParameters)
        {
            var scoreUpdateResponse = await UserGamesStaticProcessor.TryMakeAMove(authorisationDataRepository,
                selectedGameRepository.GameId, spinBoardParameters);
            _roundData.SetSlotsBoardData(scoreUpdateResponse.BoardData);
            UpdateSlotsIconsPositionsAndActivity(spinBoardParameters);
        }
        
        private void UpdateSlotsIconsPositionsAndActivity()
        {
            GameInterface.SwitchIconsInSlots(_roundData.SlotsIconsData);
            PrintLog("Slots Icons was updated");
        }

        private void UpdateSlotsIconsPositionsAndActivity(in SpinBoardParameters spinBoardParameters)
        {
            GameInterface.SetSpinTargetsAndStartSpinning(_roundData.SlotsIconsData, spinBoardParameters);
            PrintLog("Slots Icons was updated");
        }

        private static void PrintLog(string message, LogType logType = LogType.Log)
        {
            Debug.unityLogger.Log(logType, nameof(SlotsGameBehaviour), message);
        }
    }
}