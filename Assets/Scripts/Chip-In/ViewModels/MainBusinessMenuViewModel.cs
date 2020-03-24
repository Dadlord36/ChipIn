using System;
using Controllers;
using DataModels;
using DataModels.RequestsModels;
using GlobalVariables;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;

namespace ViewModels
{
    /*[Binding]
    public class MainBusinessMenuViewModel : BaseViewModel
    {
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        [SerializeField] private SessionController sessionController;

        private MainBusinessMenuView ThisView => View as MainBusinessMenuView;
        
        private DateTime InMinute
        {
            get
            {
                var now = DateTime.UtcNow;
                now = now.AddMinutes(ThisView.GameStartingTimeDelay);
                now = now.AddSeconds(1);
                return now;
            }
        }

        private  OfferCreationRequestModel Offer => new OfferCreationRequestModel
        {
            PosterAsText = Resources.Load<TextAsset>("Images/OfferPoster"),

            Offer = new UserCreatedOffer
            {
                Category = MainNames.OfferCategories.BulkOffer, Description = "Something",
                Price = 10, Quantity = 1, Segment = MainNames.OfferSegments.Food, Title = ThisView.OfferTitle,
                ChallengeType = MainNames.ChallengeTypes.Match, ExpireDate = DateTime.Today, StartedAt = InMinute
            }
        };

        [Binding]
        public void CreateOffer_OnClick()
        {
            CreateOffer();
        }

        [Binding]
        public void LogOut_OnClick()
        {
            SignOutFromAccount();
        }

        private void SignOutFromAccount()
        {
            sessionController.SignOut();
        }

        private async void CreateOffer()
        {
            try
            {
                await OffersStaticRequestProcessor.TryCreateAnOffer(userAuthorisationDataRepository, Offer);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                LogUtility.PrintLogError(nameof(MainBusinessMenuViewModel), "Can't create an offer");
            }
        }
    }*/
}