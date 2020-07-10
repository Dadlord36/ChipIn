using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using DataModels.RequestsModels;
using GlobalVariables;
using JetBrains.Annotations;
using pingak9;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Validators;
using Views;
using WebOperationUtilities;

namespace ViewModels
{
    [Binding]
    public class FlashOfferViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
              #region Serialized fields

        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;

        [SerializeField] private BaseTextValidationWithAlert validityDateInputFieldTextValidationWithAlert;
        [SerializeField] private BaseTextValidationWithAlert descriptionInputFieldTextValidationWithAlert;
        [SerializeField] private BaseTextValidationWithAlert priceInputFieldTextValidationWithAlert;

        [SerializeField] private AlertCardController _alertCardController;

        #endregion


        private bool _iconIsSelected;

        private MobileDateTimePicker _timeDataPicker;

        private readonly OfferCreationRequestModel _offerDataModel = new OfferCreationRequestModel
        {
            Offer = new UserCreatedOffer
            {
                Title = string.Empty, Description = string.Empty, Category = MainNames.OfferCategories.BulkOffer
            }
        };

        private ICreatedOfferModel ChallengingOfferDataModel => _offerDataModel.Offer;

        private CreateOfferView ThisView => View as CreateOfferView;

        #region IChallangeOffer implementatation

        [Binding]
        public string Title
        {
            get => ChallengingOfferDataModel.Title;
            set
            {
                ChallengingOfferDataModel.Title = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Description
        {
            get => ChallengingOfferDataModel.Description;
            set
            {
                ChallengingOfferDataModel.Description = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Category
        {
            get => ChallengingOfferDataModel.Category;
            set
            {
                ChallengingOfferDataModel.Category = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public DateTime ExpireDate
        {
            get => ChallengingOfferDataModel.ExpireDate;
            set
            {
                ChallengingOfferDataModel.ExpireDate = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Segment
        {
            get => ChallengingOfferDataModel.Segment;
            set
            {
                ChallengingOfferDataModel.Segment = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public uint Quantity
        {
            get => ChallengingOfferDataModel.Quantity;
            set
            {
                ChallengingOfferDataModel.Quantity = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int QuantityAsInt
        {
            get => (int) Quantity;
            set => Quantity = (uint) value;
        }

        [Binding]
        public uint Price
        {
            get => ChallengingOfferDataModel.Price;
            set
            {
                ChallengingOfferDataModel.Price = value;
                OnPropertyChanged();
            }
        }

        #endregion


        [Binding]
        public bool IconIsSelected
        {
            get => _iconIsSelected;
            set
            {
                if (value == _iconIsSelected) return;
                _iconIsSelected = value;
                OnPropertyChanged();
            }
        }


        [Binding]
        public bool CanCreateOffer
        {
            get => _canCreateOffer;
            private set
            {
                if (value == _canCreateOffer) return;
                _canCreateOffer = value;
                OnPropertyChanged();
                VerifyEnteredData();
            }
        }

        public FlashOfferViewModel() : base(nameof(FlashOfferViewModel))
        {
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            ThisView.NewCategorySelected += SetCategoryName;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ThisView.NewCategorySelected -= SetCategoryName;
            priceInputFieldTextValidationWithAlert.ValidityChanged -= VerifyEnteredData;
        }


        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
        }

        private void Start()
        {
            _timeDataPicker = MobileDateTimePicker.CreateTime();
        }

        private void SetCategoryName(string categoryName)
        {
            Segment = categoryName;
        }

        [Binding]
        public void AddPhoto_OnClick()
        {
            FillIconWithImageFromGallery();
            LogUtility.PrintLog(Tag, "AddPhoto clicked");
        }

        [Binding]
        public async void CreateOffer_OnClick()
        {
            try
            {
                CanCreateOffer = false;
                await SendCreateOfferRequest();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            finally
            {
                CanCreateOffer = true;
            }
        }

        [Binding]
        public void ShowUpDatePickerForValidityPeriod()
        {
            var now = DateTime.Now;
            _timeDataPicker.OnDateChanged = SetExpireDate;
            _timeDataPicker.OnPickerClosed = SetExpireDate;
            MobileNative.showDatePicker(now.Year, now.Month, now.Day);
        }

        private void VerifyEnteredData()
        {
            CanCreateOffer =
                validityDateInputFieldTextValidationWithAlert
                    .IsValid
                && descriptionInputFieldTextValidationWithAlert
                    .IsValid
                && Quantity > 0
                && priceInputFieldTextValidationWithAlert
                    .IsValid;
        }

        private bool _canCreateOffer;


        private void SetExpireDate(DateTime time)
        {
            ExpireDate = time.ToUniversalTime();
            ThisView.ValidityPeriod = time;
            validityDateInputFieldTextValidationWithAlert.CheckIsValid(time);
            VerifyEnteredData();
        }

        private async Task SendCreateOfferRequest()
        {
            try
            {
                await OffersStaticRequestProcessor.TryCreateAnOffer(OperationCancellationController.TasksCancellationTokenSource,
                    userAuthorisationDataRepository, _offerDataModel);
                _alertCardController.ShowAlertWithText("Offer was created");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                LogUtility.PrintLogError(Tag, "Can't create an offer");
            }
        }

        private void FillIconWithImageFromGallery()
        {
            NativeGallery.GetImageFromGallery(delegate(string path)
            {
                _offerDataModel.PosterImageFilePath = path;
                SetIconFromTexture(NativeGallery.LoadImageAtPath(path));
            });
        }

        private void SetIconFromTexture(Texture2D texture)
        {
            IconIsSelected = true;
            ThisView.AvatarIconSprite = SpritesUtility.CreateSpriteWithDefaultParameters(texture);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            VerifyEnteredData();
        }
    }
}