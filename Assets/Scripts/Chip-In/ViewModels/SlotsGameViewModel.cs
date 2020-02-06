using System;
using System.Linq;
using System.Threading.Tasks;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using HttpRequests;
using HttpRequests.RequestsProcessors.GetRequests;
using Newtonsoft.Json;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using WebOperationUtilities;
using WebSockets;

namespace ViewModels
{
    [Binding]
    public class SlotsGameViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        private GameChannelWebSocketSharp _gameChannelSocket;

        private void Start()
        {
            ApiHelper.InitializeClient();
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

        private void ConnectToTheChannel()
        {
            _gameChannelSocket.SubscribeToGameChannel();
        }

        [Binding]
        public void ConnectAndGetGameData_OnClick()
        {
            ConnectAndGetGameData();
        }
        
        private async void ConnectAndGetGameData()
        {
           await Login();
           var offerId = await LoadOffers();
           var gameId = await LoadOfferDetails(offerId);
           var matchData = await GetGameData(gameId);
           
           var textures = await matchData.MatchData.Board.GetSlotsIconsTextures();
           SetSlotsIcons(textures);
        }
        
        private async Task Login()
        {
            var response = await LoginStaticProcessor.Login(new UserLoginRequestModel
                {Email = "test@mail.com", Password = "12345678"});

            if (response.ResponseModelInterface != null && response.ResponseModelInterface.Success)
            {
                authorisationDataRepository.Set(response.ResponseModelInterface.AuthorisationData);
            }
        }
        
        private void ConnectToSocket()
        {
            StartGameSocketChannel();
        }

        private async Task<int> LoadOffers()
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
        }

        private async Task<int> LoadOfferDetails(int offerId)
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

        private void SetSlotsIcons(Texture2D[] textures)
        {
            var sprites = SpritesUtility.CreateArrayOfSpritesWithDefaultParameters(textures);
            ((SlotsGameView) View).SetSlotsIcons(sprites);
        }

        private void StartGameSocketChannel()
        {
            CloseConnectionAndDisposeGameChannelSocket();
            EstablishSocketConnection();
            SubscribeToGameSocketEvents();
        }

        private void SubscribeToGameSocketEvents()
        {
            _gameChannelSocket.MatchRoundSwitched += GameChannelSocketOnMatchRoundSwitched;
            _gameChannelSocket.RoundEnds += GameChannelSocketOnRoundEnds;
        }

        private void GameChannelSocketOnMatchRoundSwitched(MathStateData mathStateData)
        {
            throw new NotImplementedException();
        }

        private void GameChannelSocketOnRoundEnds(MathStateData mathStateData)
        {
            throw new NotImplementedException();
        }

        private void EstablishSocketConnection()
        {
            try
            {
                _gameChannelSocket = new GameChannelWebSocketSharp(authorisationDataRepository.GetRequestHeaders());
                _gameChannelSocket.Connect();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        private static void PrintLog(string message, LogType logType = LogType.Log)
        {
            Debug.unityLogger.Log(logType, "SlotsGame", message);
        }
    }
}