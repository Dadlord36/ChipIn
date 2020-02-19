using System;
using System.Globalization;
using System.Net.Http;
using System.Runtime.Serialization;
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

        private static DateTime InMinute
        {
            get
            {
                var now = DateTime.UtcNow;
                return now.AddMinutes(3);
            }
        }

        private static readonly OfferCreationRequestModel Offer = new OfferCreationRequestModel
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