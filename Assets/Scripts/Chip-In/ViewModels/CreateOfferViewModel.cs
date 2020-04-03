using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using GlobalVariables;
using JetBrains.Annotations;
using pingak9;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using WebOperationUtilities;

namespace ViewModels
{
    [Binding]
    public sealed class CreateOfferViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged, IChallengingOffer
    {
        #region Serialized fields

        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        [SerializeField] private OfferCreationRepository offerCreationRepository; 

        #endregion

        private bool _iconIsSelected;
        private const string Tag = nameof(CreateOfferViewModel);

        private MobileDateTimePicker _timeDataPicker;

        private readonly OfferCreationRequestModel _offerDataModel = new OfferCreationRequestModel
        {
            Offer = new UserCreatedOffer
            {
                Title = String.Empty, Description = String.Empty, Category = MainNames.OfferCategories.BulkOffer
            }
        };

        private IChallengingOffer ChallengingOfferDataModel => _offerDataModel.Offer;

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

        [Binding]
        public string ChallengeType
        {
            get => ChallengingOfferDataModel.ChallengeType;
            set
            {
                ChallengingOfferDataModel.ChallengeType = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public DateTime StartedAt
        {
            get => ChallengingOfferDataModel.StartedAt;
            set
            {
                ChallengingOfferDataModel.StartedAt = value;
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

        protected override void OnEnable()
        {
            base.OnEnable();
            ThisView.NewCategorySelected += SetCategoryName;
            ThisView.NewGameTypeSelected += SelectGameType;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ThisView.NewCategorySelected -= SetCategoryName;
            ThisView.NewGameTypeSelected -= SelectGameType;

        }

        private void SelectGameType(string challengeTypeName)
        {
            ChallengeType = challengeTypeName;
        }

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            Segment = offerCreationRepository.OfferSegmentName;
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
            await SendCreateOfferRequest();
        }

        [Binding]
        public void ShowUpDatePickerForValidityPeriod()
        {
            var now = DateTime.Now;
            _timeDataPicker.OnDateChanged = SetExpireDate;
            _timeDataPicker.OnPickerClosed = SetExpireDate;
            MobileNative.showDatePicker(now.Year, now.Month, now.Day);
        }

        [Binding]
        public void ShowUpDataPickerForStartingData()
        {
            var now = DateTime.Now;
            _timeDataPicker.OnDateChanged = SetStatingDate;
            _timeDataPicker.OnPickerClosed = ShowUpDataPickerForStartingTime;
            MobileNative.showDatePicker(now.Year, now.Month, now.Day);
        }

        private DateTime _startingDate;

        private void ShowUpDataPickerForStartingTime(DateTime dateTime)
        {
            _startingDate = dateTime;
            _timeDataPicker.OnDateChanged = SetStatingDate;
            _timeDataPicker.OnPickerClosed = SetStatingDate;
            MobileNative.showTimePicker();
        }

        private void SetStatingDate(DateTime time)
        {
            var startingDateTime = new DateTime(_startingDate.Year, _startingDate.Month, _startingDate.Day, time.Hour,
                time.Minute, time.Second, time.Millisecond);

            StartedAt = startingDateTime.ToUniversalTime();
            ThisView.StartingTime = startingDateTime;
        }

        private void SetExpireDate(DateTime time)
        {
            ExpireDate = time.ToUniversalTime();
            ThisView.ValidityPeriod = time;
        }


        private async Task SendCreateOfferRequest()
        {
            try
            {
                await OffersStaticRequestProcessor.TryCreateAnOffer(userAuthorisationDataRepository, _offerDataModel);
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
        }
    }
}