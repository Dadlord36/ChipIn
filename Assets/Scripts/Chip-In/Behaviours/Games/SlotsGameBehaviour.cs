using System;
using System.Threading.Tasks;
using DataModels.Interfaces;
using DataModels.MatchModels;
using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;
using ViewModels;
using WebSockets;

namespace Behaviours.Games
{
    public class SlotsGameBehaviour : MonoBehaviour
    {
        #region Public structs declaration

        public struct SlotGameRoundData
        {
            public int RoundNumber;
            public int RoundEndsInSeconds;
            public int? WinnerId;
            private SlotsBoardData _slotsBoardData;

            public ISlotIconBaseData[] SlotsIconsData => _slotsBoardData.GetIconsData();
            public MatchUserDownloadingData[] UsersData { get; private set; }

            public void SetSlotsBoardData(SlotsBoardData boardDataData)
            {
                _slotsBoardData = boardDataData;
            }

            public void Update(MatchStateData matchStateData)
            {
                _slotsBoardData = matchStateData.MatchState.Body.BoardData;
                UsersData = matchStateData.MatchState.Body.Users;
                WinnerId = matchStateData.MatchState.Body.WinnerId;
            }

            public void Update(IBaseMatchModel matchDataModel)
            {
                _slotsBoardData = matchDataModel.BoardData;
                UsersData = matchDataModel.Users;
                WinnerId = matchDataModel.WinnerId;
            }
        }

        #endregion


        #region Private serialized fields

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;
        [SerializeField] private SlotsGameViewModel gameInterfaceViewModel;

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

        private void OnEnable()
        {
            GetGameDataAndInitializeGame();
            SubscribeToGameViewEvents();
        }

        private void OnDisable()
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
            await InitializeMatch();
            await UpdateGameRepositoryUsersData();
            await StartGameSocketChannel();
            StartNewRound();
        }

        private async Task InitializeMatch()
        {
            try
            {
                var matchData =
                    await UserGamesStaticProcessor.TryShowMatch(authorisationDataRepository,
                        selectedGameRepository.GameId);
                _roundData.Update(matchData.MatchData);

                await GameInterface.RefillIconsSet(matchData.MatchData.IndexedSpritesSheetsUrls);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        private void StartNewRound()
        {
            RoundNumber = _roundData.RoundNumber;
            UpdateSlotsIconsPositionsAndActivity();
            GameInterface.StartTimer(_roundData.RoundEndsInSeconds);
            GameInterface.AllowInteractivity();
        }

        private void FinishTheGame()
        {
            _gameShouldBeFinished = false;
            PrintLog("Game is over");
            GameInterface.OnGameFinished();
        }

        private void Update()
        {
            if (_roundEnds)
            {
                UpdateUsersData();
                StartNewRound();
                _roundEnds = false;
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
            _gameShouldBeFinished = true;
            selectedGameRepository.WinnerId = matchStateData.MatchState.Body.WinnerId;
        }

        private void UdataRoundData(MatchStateData matchStateData)
        {
            _roundEnds = true;
            _roundData.Update(matchStateData);
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
            var scoreUpdateResponse = await UserGamesStaticProcessor.TryMakeAMove(authorisationDataRepository,
                selectedGameRepository.GameId,
                spinBoardParameters);
            _roundData.SetSlotsBoardData(scoreUpdateResponse.BoardData);
            UpdateSlotsIconsPositionsAndActivity();
        }

        private static bool GameIsInProgress(IGameModel gameModel)
        {
            return gameModel.Status == "in_progress";
        }

        private void UpdateSlotsIconsPositionsAndActivity()
        {
            GameInterface.SetSlotsIcons(_roundData.SlotsIconsData);
            PrintLog("Slots Icons was updated");
        }

        private static void PrintLog(string message, LogType logType = LogType.Log)
        {
            Debug.unityLogger.Log(logType, nameof(SlotsGameBehaviour), message);
        }
    }
}