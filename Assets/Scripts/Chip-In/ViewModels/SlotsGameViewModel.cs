using System;
using System.Linq;
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
using WebSocket4Net;
using WebSockets;

namespace ViewModels
{
    [Binding]
    public class SlotsGameViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private int gameId;

        private int _offerId;
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

        [Binding]
        public void Login_OnClick()
        {
            Login();
        }

        [Binding]
        public void ConnectSocket_OnClick()
        {
            ConnectToSocket();
        }

        [Binding]
        public void LoadOffersData_OnClick()
        {
            LoadOffers();
        }

        [Binding]
        public void LoadOfferDetails_OnClick()
        {
            LoadOfferDetails();
        }

        [Binding]
        public void GetGameData_OnClick()
        {
            GetGameData();
        }

        [Binding]
        public void ConnectToTheChannel_OnClick()
        {
            ConnectToTheChannel();
        }

        private void ConnectToTheChannel()
        {
            _gameChannelSocket.SubscribeToGameChannel();
        }

        private async void Login()
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

        private async void LoadOffers()
        {
            var offersData = await OffersStaticRequestProcessor.GetListOfOffers(authorisationDataRepository);
            if (offersData == null || !offersData.Any())
            {
                PrintLog("There is no offers in the offers list", LogType.Error);
                return;
            }

            foreach (var offer in offersData)
            {
                PrintLog(JsonConvert.SerializeObject(offer));
            }

            _offerId = offersData[0].Id;
        }

        private async void LoadOfferDetails()
        {
            var offerData =
                await OffersStaticRequestProcessor.GetOfferDetails(
                    new DetailedOfferGetProcessor.DetailedOfferGetProcessorParameters(authorisationDataRepository,
                        _offerId));

            gameId = offerData.Offer.GameData.Id;
            if (!GameIsInProgress(offerData.Offer.GameData))
            {
                PrintLog("Game is not in progress", LogType.Error);
            }

            PrintLog($"Game will starts at {offerData.Offer.GameData.StartedAt}");
        }

        private static bool GameIsInProgress(IGameData gameData)
        {
            return gameData.Status == "in_progress";
        }

        private async void GetGameData()
        {
            var matchData = await UserGamesStaticProcessor.ShowMatch(authorisationDataRepository, gameId);
            if (matchData == null) return;
            PrintLog(JsonConvert.SerializeObject(matchData));
        }

        private void StartGameSocketChannel()
        {
            CloseConnectionAndDisposeGameChannelSocket();
            EstablishSocketConnection();
            SubscribeToGameSocketEvents();
        }

        private void SubscribeToGameSocketEvents()
        {
            
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