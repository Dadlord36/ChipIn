using System;
using System.Globalization;
using DataModels;
using DataModels.RequestsModels;
using GlobalVariables;
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
                return now.AddMonths(1);
            }
        }

        private static OfferCreationRequestModel Offer = new OfferCreationRequestModel
        {
            Offer = new UserCreatedOffer
            {
                Category = MainNames.OfferCategories.BulkOffer, Description = "Something",
                Price = 10, Quantity = 1, Segment = MainNames.OfferSegments.Food, Title = "Title",
                User = new OfferCreatorDataModel(), ChallengeType = MainNames.ChallengeTypes.Match,
                ExpireDate = DateTime.Today.ToString(CultureInfo.InvariantCulture),
                PosterUri = "https://dummyimage.com/600x400/000/fff",
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
                await OffersStaticRequestProcessor.CreateAnOffer(userAuthorisationDataRepository, Offer);
            }
            catch
            {
                LogUtility.PrintLogError(nameof(MainBusinessMenuViewModel), "Can't create an offer");
            }
        }
    }
}