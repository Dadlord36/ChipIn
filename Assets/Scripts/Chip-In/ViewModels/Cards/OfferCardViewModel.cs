using System;
using System.Threading.Tasks;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using Factories;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Cards
{
    public interface ITokensDedicationConfirmation
    {
        void OnTokensDedicationConfirmation();
    }

    [Binding]
    public sealed class OfferCardViewModel : ListItemBase<ClientOfferDataModel>, ITokensDedicationConfirmation
    {
        private string _description;
        private Sprite _poster;
        private uint _price;
        private uint _realPrice;
        private DateTime _validityPeriod;

        public event Action<OfferCardViewModel> BuyButtonPressed;

        [Binding]
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        private Sprite _logoSprite;
        private string _titleOfOffer;

        [Binding]
        public Sprite LogoSprite
        {
            get => _logoSprite;
            set
            {
                _logoSprite = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite Poster
        {
            get => _poster;
            set
            {
                _poster = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public uint Price
        {
            get => _price;
            set
            {
                if (value == _price) return;
                _price = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public uint RealPrice
        {
            get => _realPrice;
            set
            {
                if (value == _realPrice) return;
                _realPrice = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public DateTime ValidityPeriod
        {
            get => _validityPeriod;
            set
            {
                if (value == _validityPeriod) return;
                _validityPeriod = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string TitleOfOffer
        {
            get => _titleOfOffer;
            set
            {
                if (value == _titleOfOffer) return;
                _titleOfOffer = value;
                OnPropertyChanged();
            }
        }

        private static IRequestHeaders AuthorisationRequestHeaders => SimpleAutofac.GetInstance<IUserAuthorisationDataRepository>();
        private static IAlertCardController AlertCardController => SimpleAutofac.GetInstance<IAlertCardController>();

        private OfferCardViewModel() : base(nameof(OfferCardViewModel))
        {
        }

        private int _offerId;

        public override async Task FillView(ClientOfferDataModel data, uint dataBaseIndex)
        {
            AsyncOperationCancellationController.CancelOngoingTask();

            Description = data.Description;
            Price = data.Price;
            RealPrice = data.RealPrice;
            ValidityPeriod = data.ExpireDate;
            TitleOfOffer = data.Title;
            _offerId = (int) data.Id;

            try
            {
                var downloadedSpritesRepository = SimpleAutofac.GetInstance<IDownloadedSpritesRepository>();
                Poster = await downloadedSpritesRepository.CreateLoadSpriteTask(data.PosterUri, AsyncOperationCancellationController.CancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public void BuyButton_OnClick()
        {
            OnBuyButtonPressed();
        }

        public async void OnTokensDedicationConfirmation()
        {
            try
            {
                var result = await OffersStaticRequestProcessor.BuyAnOffer(out AsyncOperationCancellationController.TasksCancellationTokenSource,
                        AuthorisationRequestHeaders, _offerId)
                    .ConfigureAwait(false);

                AlertCardController.ShowAlertWithText(result.Success ? "Offer was purchased successfully" : result.Error);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private void OnBuyButtonPressed()
        {
            BuyButtonPressed?.Invoke(this);
        }
    }
}