using DataModels.RequestsModels;
using HttpRequests;
using HttpRequests.RequestsProcessors.GetRequests;
using Newtonsoft.Json;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public class SlotsGameViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private int gameId;
        private int _offerId;

        private void Start()
        {
            ApiHelper.InitializeClient();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ApiHelper.Dispose();
        }

        [Binding]
        public void Login_OnClick()
        {
            Login();
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


        private async void Login()
        {
            var response = await LoginStaticProcessor.Login(new UserLoginRequestModel
                {Email = "test@mail.com", Password = "12345678"});

            if (response.ResponseModelInterface != null && response.ResponseModelInterface.Success)
            {
                authorisationDataRepository.Set(response.ResponseModelInterface.AuthorisationData);
            }
        }

        private async void LoadOffers()
        {
            var offersData = await OffersStaticRequestProcessor.GetListOfOffers(authorisationDataRepository);
            foreach (var offer in offersData)
            {
                Debug.Log(JsonConvert.SerializeObject(offer));
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
        }

        private async void GetGameData()
        {
            var matchData = await UserGamesStaticProcessor.ShowMatch(authorisationDataRepository, gameId);
            Debug.Log(JsonConvert.SerializeObject(matchData));
        }
    }
}