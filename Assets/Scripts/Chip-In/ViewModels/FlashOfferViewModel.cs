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
using UnityWeld.Binding;
using Utilities;
using ViewModels.UI.Elements;

namespace ViewModels
{
    [Binding]
    public sealed class FlashOfferViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        #region Serialized fields

        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;

        #endregion

        private readonly FlashOfferCreationRequestDataModel _flashOfferCreationRequestDataModel =
            new FlashOfferCreationRequestDataModel {FlashOffer = new FlashOfferGetRequestDataModel()};

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
            set
            {
                if (PosterFilePath == value) return;
                FlashOfferCreation.PosterFilePath = new FilePath(value);
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Title
        {
            get => FlashOfferData.Title;
            set
            {
                if (Title == value) return;
                FlashOfferData.Title = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Description
        {
            get => FlashOfferData.Description;
            set
            {
                if (FlashOfferData.Description == value) return;
                FlashOfferData.Description = value;
                OnPropertyChanged();
            }
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
            set
            {
                if (Quantity == value) return;
                FlashOfferData.Quantity = value;
                OnPropertyChanged();
            }
        }

        public string Radius
        {
            get => FlashOfferData.Radius;
            set
            {
                if (Radius == value) return;
                FlashOfferData.Radius = value;
                OnPropertyChanged();
            }
        }


        [Binding]
        public int QuantityAsInt
        {
            get => (int) Quantity;
            set
            {
                if (QuantityAsInt == value) return;
                Quantity = (uint) value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Price
        {
            get => FlashOfferData.Price.ToString();
            set
            {
                if (Price == value) return;
                FlashOfferData.Price = uint.Parse(value);
                OnPropertyChanged();
            }
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            Description = string.Empty;
            Price = "0";
            QuantityAsInt = 0;
            Title = string.Empty;
            ExpireLocalDate = DateTime.Now;
            SelectedCurrencyTypeIndex = 0;
            SelectedOfferTypeIndex = 0;
            transform.GetComponentInChildren<TimePickerAreaViewModel>().Clear();
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
                FlashOfferData.Radius = 5.ToString();
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