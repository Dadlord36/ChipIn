using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using DataModels.SimpleTypes;
using GlobalVariables;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using UnityEngine.Serialization;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels
{
    [Binding]
    public sealed class FlashOfferViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        #region Serialized fields

        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;

        #endregion

        private readonly FlashOfferGetRequestDataModel _flashOfferGetRequestData = new FlashOfferGetRequestDataModel();
        private readonly FlashOfferCreationRequestDataModel _flashOfferCreationRequestDataModel = new FlashOfferCreationRequestDataModel();

        private IFlashOfferCreationRequestModel FlashOfferCreation => _flashOfferCreationRequestDataModel;
        private IFlashOfferGetRequestModel FlashOfferData => FlashOfferCreation.FlashOffer;

        private bool _canCreateOffer;
        private DateTime _expireLocalDate;

        private int _selectedCurrencyTypeIndex;
        private int _selectedOfferTypeIndex;


        #region IChallangeOffer implementatation

        [Binding]
        public string PosterFilePath
        {
            get => FlashOfferCreation.PosterFilePath.Path;
            set => FlashOfferCreation.PosterFilePath = new FilePath(value);
        }

        [Binding]
        public string Title
        {
            get => FlashOfferData.Title;
            set => FlashOfferData.Title = value;
        }

        [Binding]
        public string Description
        {
            get => FlashOfferData.Description;
            set => FlashOfferData.Description = value;
        }

        [Binding]
        public DateTime ExpireLocalDate
        {
            get => _expireLocalDate;
            set
            {
                if (value.Equals(_expireLocalDate)) return;
                _expireLocalDate = value;
                ExpireDate = value.ToUniversalTime();
                OnPropertyChanged();
            }
        }

        public DateTime ExpireDate
        {
            get => FlashOfferData.ExpireDate;
            set => FlashOfferData.ExpireDate = value;
        }


        [Binding]
        public uint Quantity
        {
            get => FlashOfferData.Quantity;
            set => FlashOfferData.Quantity = value;
        }

        public string Radius
        {
            get => FlashOfferData.Radius;
            set => FlashOfferData.Radius = value;
        }


        [Binding]
        public int QuantityAsInt
        {
            get => (int) Quantity;
            set => Quantity = (uint) value;
        }

        [Binding]
        public string Price
        {
            get => FlashOfferData.Price.ToString();
            set => FlashOfferData.Price = uint.Parse(value);
        }

        #endregion

        [Binding]
        public bool CanCreateOffer
        {
            get => _canCreateOffer;
            private set
            {
                if (value == _canCreateOffer) return;
                _canCreateOffer = value;
                OnPropertyChanged();
            }
        }

        #region Offer Life Tipe

        [Binding]
        public int SelectedOfferTypeIndex
        {
            get => _selectedOfferTypeIndex;
            set
            {
                _selectedOfferTypeIndex = value;
                SelectedOfferType = MainNames.OfferLifeTypes.GetOfferLifeTypeName(value);
            }
        }

        [Binding]
        public string SelectedOfferType
        {
            get => FlashOfferData.Period;
            set
            {
                if (SelectedOfferType == value) return;
                FlashOfferData.Period = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Currency Type

        [Binding]
        public int SelectedCurrencyTypeIndex
        {
            get => _selectedCurrencyTypeIndex;
            set
            {
                _selectedCurrencyTypeIndex = value;
                SelectedCurrencyType = MainNames.CurrencyNames.GetCurrencyName(value);
            }
        }

        [Binding]
        public string SelectedCurrencyType
        {
            get => FlashOfferData.PriceType;
            set
            {
                FlashOfferData.PriceType = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public FlashOfferViewModel() : base(nameof(FlashOfferViewModel))
        {
            _flashOfferGetRequestData.PropertyChanged += PropertyChanged;
            FlashOfferCreation.FlashOffer = _flashOfferGetRequestData;
            Radius = "2";
        }

        [Binding]
        public async void CreateOffer_OnClick()
        {
            try
            {
                if (!ValidationHelper.CheckIfAllFieldsAreValid(this))
                {
                    return;
                }

                CanCreateOffer = false;
                IsAwaitingProcess = true;
                await SendCreateOfferRequestAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            finally
            {
                IsAwaitingProcess = false;
                CanCreateOffer = true;
            }
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            SelectedCurrencyTypeIndex = 0;
            SelectedOfferTypeIndex = 0;
        }

        private async Task SendCreateOfferRequestAsync()
        {
            try
            {
                OperationCancellationController.CancelOngoingTask();

                var response = await OffersStaticRequestProcessor.CreateFlashOffer(OperationCancellationController.TasksCancellationTokenSource,
                    userAuthorisationDataRepository, FlashOfferCreation).ConfigureAwait(false);

                alertCardController.ShowAlertWithText(response.IsSuccessful ? "Offer was created" : "Offer was not created");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                LogUtility.PrintLogError(Tag, "Can't create an offer");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}