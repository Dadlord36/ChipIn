using System;
using System.Globalization;
using System.Net.Http;
using System.Runtime.Serialization;
using Controllers;
using DataModels;
using DataModels.RequestsModels;
using DataModels.SimpleTypes;
using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels
{
    [Binding]
    public class MainBusinessMenuViewModel : BaseViewModel
    {
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        [SerializeField] private SessionController _sessionController;
        [SerializeField] private int startingAfterMinutes = 1;

        private DateTime InMinute
        {
            get
            {
                var now = DateTime.UtcNow;
                now = now.AddMinutes(startingAfterMinutes);
                now = now.AddSeconds(1);
                return now;
            }
        }

        private  OfferCreationRequestModel Offer => new OfferCreationRequestModel
        {
            PosterFilePath = new FilePath(
                @"C:\Users\Dadlo\Documents\UnityProjects\chip-in\Assets\UIDesign\MyChallenges\Nike.png"),

            Offer = new UserCreatedOffer
            {
                Category = MainNames.OfferCategories.BulkOffer, Description = "Something",
                Price = 10, Quantity = 1, Segment = MainNames.OfferSegments.Food, Title = "Title",
                ChallengeType = MainNames.ChallengeTypes.Match,
                ExpireDate = DateTime.Today,
                StartedAt = InMinute
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
            _sessionController.SignOut();
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
    }
}