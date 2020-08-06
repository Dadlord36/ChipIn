using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using DataModels.SimpleTypes;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels
{
    [Binding]
    public class FlashOfferViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        #region Serialized fields

        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        [SerializeField] private AlertCardController _alertCardController;

        #endregion

        private readonly FlashOfferGetRequestDataModel _flashOfferGetRequestDataModel = new FlashOfferGetRequestDataModel();
        private IFlashOfferGetRequestModel FlashOfferData => _flashOfferGetRequestDataModel;

        private string _posterFilePath;
        private bool _canCreateOffer;
        private DateTime _expireLocalDate;

        private readonly AsyncOperationCancellationController _cancellationController = new AsyncOperationCancellationController();

        #region IChallangeOffer implementatation
        
        
        [Binding]
        public string PosterFilePath
        {
            get => _posterFilePath;
            set => _posterFilePath = value;
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
        public string Category
        {
            get => FlashOfferData.Category;
            set => FlashOfferData.Category = value;
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
            get => _flashOfferGetRequestDataModel.Radius;
            set => _flashOfferGetRequestDataModel.Radius = value;
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
            get => FlashOfferData.TokensAmount.ToString() ;
            set => FlashOfferData.TokensAmount = uint.Parse(value);
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

        public FlashOfferViewModel() : base(nameof(FlashOfferViewModel))
        {
            _flashOfferGetRequestDataModel.PropertyChanged += PropertyChanged;
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
                _cancellationController.CancelOngoingTask();
                
                var response = await OffersStaticRequestProcessor.CreateFlashOffer(_cancellationController.TasksCancellationTokenSource,
                    userAuthorisationDataRepository, new FlashOfferCreationRequestDataModel
                    {
                        FlashOffer = FlashOfferData, PosterFilePath = new FilePath(PosterFilePath)
                    });

                if (response.IsSuccessful)
                {
                    _alertCardController.ShowAlertWithText("Offer was created");
                }
                else
                {
                    _alertCardController.ShowAlertWithText("Offer was not created");
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                LogUtility.PrintLogError(Tag, "Can't create an offer");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}