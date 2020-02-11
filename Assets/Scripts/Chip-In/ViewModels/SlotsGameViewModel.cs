using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DataModels.Interfaces;
using DataModels.MatchModels;
using DataModels.RequestsModels;
using HttpRequests;
using HttpRequests.RequestsProcessors.GetRequests;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using WebSockets;

namespace ViewModels
{
    [Binding]
    public class SlotsGameViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private class BoardIconsHolder
        {
            private readonly List<BoardIcon> _boardIcons = new List<BoardIcon>();

            public BoardIcon[] BoardIcons
            {
                set
                {
                    _boardIcons.Clear();
                    _boardIcons.Capacity = value.Length;
                    _boardIcons.AddRange(value);
                }
            }

            public Sprite[] IconsSprites
            {
                get
                {
                    var sprites = new Sprite[_boardIcons.Count];
                    for (int i = 0; i < _boardIcons.Count; i++)
                    {
                        sprites[i] = _boardIcons[i].IconSprite;
                    }

                    return sprites;
                }
            }

            private Sprite GetSpriteWithId(int index)
            {
                return _boardIcons.Find(icon => icon.Id == index).IconSprite;
            }

            public Sprite[] GetCorrespondingIconsSprites(IReadOnlyList<IIconIdentifier> identifiers)
            {
                var sprites = new Sprite[_boardIcons.Count];
                for (int i = 0; i < identifiers.Count; i++)
                {
                    sprites[i] = GetSpriteWithId(identifiers[i].IconId);
                }

                return sprites;
            }
        }

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;

        private GameChannelWebSocketSharp _gameChannelSocket;
        private readonly BoardIconsHolder _boardIconsHolder = new BoardIconsHolder();
        private IMatchModel _matchData;
        private bool _shouldUpdateSlotsIcons;
        private bool _gameShouldBeFinished;
        private int _roundNumber;

        private BoardIcon[] BoardIcons
        {
            set => _boardIconsHolder.BoardIcons = value;
        }

        private SlotsBoard Board
        {
            get => _matchData.Board;
            set => _matchData.Board = value;
        }
        
        [Binding]
        public int RoundNumber
        {
            get => _roundNumber;
            set
            {
                if (value == _roundNumber) return;
                _roundNumber = value;
                OnPropertyChanged();
            }
        }

        private ISlotIconBaseData[] SlotIconData => _matchData.Board.IconsData;

        protected override void OnEnable()
        {
            base.OnEnable();
            GetGameDataAndInitializeGame();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            CloseConnectionAndDisposeGameChannelSocket();
        }

        private void CloseConnectionAndDisposeGameChannelSocket()
        {
            if (_gameChannelSocket == null) return;

            _gameChannelSocket.Close();
            (_gameChannelSocket as IDisposable).Dispose();
        }

        [Binding]
        public void SpinFrame_OnClick()
        {
            SpinFrame();
        }

        [Binding]
        public void SpinBoard_OnClick()
        {
            SpinBoard();
        }

        private async Task UpdateMatchData()
        {
            var result = await GetGameData(selectedGameRepository.GameId);
            _matchData = result.MatchData;
        }

        private async void GetGameDataAndInitializeGame()
        {
            await UpdateMatchData();
            await UpdateGameRepositoryUsersData();
            await LoadBoardIcons();
            UpdateSlotsIconsPositionsAndActivity();
            await StartGameSocketChannel();
        }

        private async Task UpdateGameRepositoryUsersData()
        {
            await selectedGameRepository.SaveUsersData(_matchData);
        }

        private async Task LoadBoardIcons()
        {
            BoardIcons = await Board.GetBoardIcons();
        }

        private void UpdateSlotsIconsPositionsAndActivity()
        {
            SetSlotsIcons(_boardIconsHolder.GetCorrespondingIconsSprites(SlotIconData));
            UpdateSlotsIconsFramesActivity(SlotIconData);
            LogUtility.PrintLog(tag, "Slots Icons was updated");
        }

        private static bool GameIsInProgress(IGameData gameData)
        {
            return gameData.Status == "in_progress";
        }

        private async Task<IShowMatchResponseModel> GetGameData(int gameId)
        {
            var matchData = await UserGamesStaticProcessor.ShowMatch(authorisationDataRepository, gameId);
            if (matchData == null) return null;
            PrintLog(JsonConvert.SerializeObject(matchData));
            return matchData;
        }

        private void UpdateSlotsIconsFramesActivity(IReadOnlyList<IActive> iconsActivity)
        {
            SetSlotsActivity(iconsActivity);
            LogUtility.PrintLog(tag, "Slots Icons Activity State was updated");
        }

        private void SetSlotsActivity(IReadOnlyList<IActive> iconsActivity)
        {
            ((SlotsGameView) View).SetSlotsActivity(iconsActivity);
        }

        private void SetSlotsIcons(Sprite[] sprites)
        {
            ((SlotsGameView) View).SetSlotsIcons(sprites);
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

        private void GameChannelSocketOnMatchRoundEnds(MatchStateData matchStateData)
        {
            PrepareMatchStateDataForIconsUpdate(matchStateData);
            RoundNumber = matchStateData.MatchState.Round;
        }

        private void GameChannelSocketOnMatchEnds(MatchStateData matchStateData)
        {
            _gameShouldBeFinished = true;
            selectedGameRepository.WinnerId = matchStateData.MatchState.Body.WinnerId;
        }

        private void PrepareMatchStateDataForIconsUpdate(MatchStateData matchStateData)
        {
            _shouldUpdateSlotsIcons = true;
            Board = matchStateData.MatchState.Body.Board;
        }


        private void FinishTheGame()
        {
            PrintLog("Game is over");
            SwitchToView(nameof(WinnerView));
            _gameShouldBeFinished = false;
        }

        private void Update()
        {
            if (_shouldUpdateSlotsIcons)
                UpdateSlotsIconsFromMainThread();
            if (_gameShouldBeFinished)
                FinishTheGame();
        }

        private async Task MakeASpin(SpinBoardParameters spinBoardParameters)
        {
            IUpdateUserScoreResponseModel userScore =
                await UserGamesStaticProcessor.MakeAMove(authorisationDataRepository, selectedGameRepository.GameId,
                    spinBoardParameters);
            PrintLog("User spin complete");
            Board = userScore.Board;
            UpdateSlotsIconsPositionsAndActivity();
        }

        private void UpdateSlotsIconsFromMainThread()
        {
            UpdateSlotsIconsPositionsAndActivity();
            _shouldUpdateSlotsIcons = false;
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

        private void SpinFrame()
        {
            MakeASpin(SpinBoardParameters.JustFrame);
        }

        private void SpinBoard()
        {
            MakeASpin(SpinBoardParameters.JustBoard);
        }

        private static void PrintLog(string message, LogType logType = LogType.Log)
        {
            Debug.unityLogger.Log(logType, "SlotsGame", message);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}