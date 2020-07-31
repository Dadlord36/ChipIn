using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using DataModels.RequestsModels;
using GlobalVariables;
using JetBrains.Annotations;
using pingak9;
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
        
        private MobileDateTimePicker _timeDataPicker;
        


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
        

        protected override void OnDisable()
        {
            base.OnDisable();
            priceInputFieldTextValidationWithAlert.ValidityChanged -= VerifyEnteredData;
        }

        private void Start()
        {
            _timeDataPicker = MobileDateTimePicker.CreateTime();
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
            /*CanCreateOffer =
                validityDateInputFieldTextValidationWithAlert
                    .IsValid
                && descriptionInputFieldTextValidationWithAlert
                    .IsValid
                && Quantity > 0
                && priceInputFieldTextValidationWithAlert
                    .IsValid;*/
        }

        private bool _canCreateOffer;


        private void SetExpireDate(DateTime time)
        {
            /*ExpireDate = time.ToUniversalTime();
            ThisView.ValidityPeriod = time;*/
            validityDateInputFieldTextValidationWithAlert.CheckIsValid(time);
            VerifyEnteredData();
        }

        private async Task SendCreateOfferRequest()
        {
            try
            {
              //ToDO: implement creation functionality
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            VerifyEnteredData();
        }
    }
}