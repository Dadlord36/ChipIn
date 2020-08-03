using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using pingak9;
using Repositories.Remote;
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

        private bool _canCreateOffer;


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
        }
    }
}