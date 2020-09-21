﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using DataModels.RequestsModels;
using GlobalVariables;
using JetBrains.Annotations;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class CreateOfferViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        #region Serialized fields

        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        [SerializeField] private OfferCreationRepository offerCreationRepository;

        [SerializeField] private AlertCardController _alertCardController;

        #endregion

        private bool _canCreateOffer;
        private DateTime _expireLocalDate;
        private string _posterFilePath;
        private int _selectedCurrencyTypeIndex;
        private string _expireLocalDateAsString;
        private string _selectedCurrencyType;


        private readonly OfferCreationRequestModel _offerDataModel = new OfferCreationRequestModel
        {
            Offer = new UserCreatedOffer
            {
                Title = string.Empty, Description = string.Empty, Category = MainNames.OfferCategories.BulkOffer
            }
        };

        private ICreatedOfferModel ChallengingOfferDataModel => _offerDataModel.Offer;

        private CreateOfferView ThisView => View as CreateOfferView;


        [Binding]
        public string PosterFilePath
        {
            get => _posterFilePath;
            set
            {
                if (value == _posterFilePath) return;
                _posterFilePath = value;
                OnPropertyChanged();
            }
        }

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

        public bool AllFieldsAreValid { get; set; }

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
        public string Price
        {
            get => ChallengingOfferDataModel.Price.ToString();
            set
            {
                ChallengingOfferDataModel.Price = uint.Parse(value);
                OnPropertyChanged();
            }
        }

        [Binding]
        public string SelectedCurrencyType
        {
            get => _selectedCurrencyType;
            set
            {
                _selectedCurrencyType = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int SelectedCurrencyTypeIndex
        {
            get => _selectedCurrencyTypeIndex;
            set
            {
                _selectedCurrencyTypeIndex = value;
                switch (value)
                {
                    case 0:
                    {
                        SelectedCurrencyType = "Cash";
                        return;
                    }
                    case 1:
                    {
                        SelectedCurrencyType = "Tokens";
                        return;
                    }
                    case 2:
                    {
                        SelectedCurrencyType = "Cash / Tokens";
                        return;
                    }
                }
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
            }
        }

        public CreateOfferViewModel() : base(nameof(CreateOfferViewModel))
        {
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            SelectedCurrencyTypeIndex = 0;
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
        }


        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            Segment = offerCreationRepository.OfferSegmentName;
        }


        private void SetCategoryName(string categoryName)
        {
            Segment = categoryName;
        }

        [Binding]
        public async void CreateOffer_OnClick()
        {
            if (!ValidationHelper.CheckIfAllFieldsAreValid(this))
            {
                return;
            }

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

        private async Task SendCreateOfferRequest()
        {
            try
            {
                await OffersStaticRequestProcessor.TryCreateAnOffer(OperationCancellationController.TasksCancellationTokenSource,
                    userAuthorisationDataRepository, _offerDataModel).ConfigureAwait(true);
                _alertCardController.ShowAlertWithText("Offer was created");
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