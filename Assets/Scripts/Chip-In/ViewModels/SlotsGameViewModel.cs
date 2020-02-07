using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataModels.Interfaces;
using DataModels.MatchModels;
using DataModels.RequestsModels;
using HttpRequests;
using HttpRequests.RequestsProcessors.GetRequests;
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
    public class SlotsGameViewModel : ViewsSwitchingViewModel
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

            public Sprite[] GetCorrespondingIconsSprites(IReadOnlyList<int> indexes)
            {
                var sprites = new Sprite[_boardIcons.Count];
                for (int i = 0; i < indexes.Count; i++)
                {
                    sprites[i] = GetSpriteWithId(indexes[i]);
                }

                return sprites;
            }
        }

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;

        private GameChannelWebSocketSharp _gameChannelSocket;
        private readonly BoardIconsHolder _boardIconsHolder = new BoardIconsHolder();

        private void Start()
        {
            ApiHelper.InitializeClient();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GetGameDataAndInitializeGame();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ApiHelper.Dispose();
            CloseConnectionAndDisposeGameChannelSocket();
        }

        private void CloseConnectionAndDisposeGameChannelSocket()
        {
            if (_gameChannelSocket == null) return;

            _gameChannelSocket.Close();
            (_gameChannelSocket as IDisposable).Dispose();
        }

        [Binding]
        public void Spin_OnClick()
        {
            MakeASpin();
        }

        private async void GetGameDataAndInitializeGame()
        {
            var matchData = await GetGameData(selectedGameRepository.GameId);

            _boardIconsHolder.BoardIcons = await matchData.MatchData.Board.GetBoardIcons();
            UpdateSlotsIcons(matchData.MatchData.Board.IconsIndexes);

            await StartGameSocketChannel();
        }


        /*private async Task Login()
        {
            var response = await LoginStaticProcessor.Login(new UserLoginRequestModel
                {Email = "test@mail.com", Password = "12345678"});

            if (response.ResponseModelInterface != null && response.ResponseModelInterface.Success)
            {
                authorisationDataRepository.Set(response.ResponseModelInterface.AuthorisationData);
            }
        }*/

        /*private async Task<int> LoadOffers()
        {
            var offersData = await OffersStaticRequestProcessor.GetListOfOffers(authorisationDataRepository);
            if (offersData == null || !offersData.Any())
            {
                PrintLog("There is no offers in the offers list", LogType.Error);
                return -1;
            }

            foreach (var offer in offersData)
            {
                PrintLog(JsonConvert.SerializeObject(offer));
            }

            return offersData[0].Id;
        }*/

        /*private async Task<int> LoadOfferDetails(int offerId)
        {
            var offerData =
                await OffersStaticRequestProcessor.GetOfferDetails(
                    new DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters(authorisationDataRepository,
                        offerId));


            if (!GameIsInProgress(offerData.Offer.GameData))
            {
                PrintLog("Game is not in progress", LogType.Error);
            }

            PrintLog($"Game will starts at {offerData.Offer.GameData.StartedAt}");
            return offerData.Offer.GameData.Id;
        }*/

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

        private void UpdateSlotsIcons(IReadOnlyList<int> iconsIndexes)
        {
            SetSlotsIcons(_boardIconsHolder.GetCorrespondingIconsSprites(iconsIndexes));
            LogUtility.PrintLog(tag, "Slots Icons was updated");
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

        private MatchStateData _matchStateData;
        private bool _shouldUpdateSlotsIcons;

        private void GameChannelSocketOnMatchRoundEnds(MatchStateData matchStateData)
        {
            PrepareMatchStateDataForIconsUpdate(matchStateData);
        }

        private void PrepareMatchStateDataForIconsUpdate(MatchStateData matchStateData)
        {
            _matchStateData = matchStateData;
            _shouldUpdateSlotsIcons = _matchStateData != null;
        }

        private void Update()
        {
            UpdateSlotsIconsFromMatchStateData();
        }

        private void UpdateSlotsIconsFromMatchStateData()
        {
            if (!_shouldUpdateSlotsIcons) return;
            UpdateSlotsIcons(_matchStateData.MatchState.Body.Board.IconsIndexes);
            _shouldUpdateSlotsIcons = false;
        }

        private void GameChannelSocketOnMatchEnds(MatchStateData matchStateData)
        {
            PrintLog("Game over");
            SwitchToView(nameof(WinnerView));
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

        private async Task MakeASpin()
        {
            var userScore =
                await UserGamesStaticProcessor.MakeAMove(authorisationDataRepository, selectedGameRepository.GameId);
        }

        private static void PrintLog(string message, LogType logType = LogType.Log)
        {
            Debug.unityLogger.Log(logType, "SlotsGame", message);
        }
    }
}