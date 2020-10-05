using System;
using System.Collections.Generic;
using System.ComponentModel;
using DataModels.Interfaces;
using GlobalVariables;
using Repositories.Local;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.OptionsSelectionViews;

namespace ViewModels.Settings
{
    [Binding]
    public class UserProfileViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private ScriptableUserProfileRemoteRepository repository;
        [SerializeField] private GeoLocationRepository geoLocationRepository;
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;

        private IUserProfileModel UserSettingsModel => repository;

        private int _selectedCountryIndex;
        private int _selectedCurrencyIndex;

        public int SelectedCountryIndex
        {
            get => _selectedCountryIndex;
            set
            {
                _selectedCountryIndex = value;
                switch (value)
                {
                    case 0:
                    {
                        CountryCode = "canada";
                        return;
                    }
                    case 1:
                    {
                        CountryCode = "usa";
                        return;
                    }
                    case 2:
                    {
                        CountryCode = "england";
                        return;
                    }
                }
            }
        }

        public int SelectedCurrencyIndex
        {
            get => _selectedCurrencyIndex;
            set
            {
                _selectedCurrencyIndex = value;
                switch (value)
                {
                    case 0:
                    {
                        CurrencyCode = "cad";
                        return;
                    }
                    case 1:
                    {
                        CurrencyCode = "usd";
                        return;
                    }
                    case 2:
                    {
                        CurrencyCode = "gbr";
                        return;
                    }
                }
            }
        }


        [Binding] public Sprite AvatarImageSprite => repository.UserAvatarSprite;


        [Binding]
        public string Name
        {
            get => UserSettingsModel.Name;
            set => UserSettingsModel.Name = value;
        }

        [Binding]
        public int Id
        {
            get => UserSettingsModel.Id ?? 0;
            set => UserSettingsModel.Id = value;
        }

        [Binding]
        public string Email
        {
            get => UserSettingsModel.Email;
            set => UserSettingsModel.Email = value;
        }


        [Binding]
        public string Birthday
        {
            get => UserSettingsModel.Birthday;
            set => UserSettingsModel.Birthday = value;
        }


        [Binding]
        public string CountryCode
        {
            get => UserSettingsModel.CountryCode;
            set
            {
                if(value == CountryCode) return;
                UserSettingsModel.CountryCode = value;
                SyncChangedPropertyWithServer(value, MainNames.ModelsPropertiesNames.Country);
            }
        }

        [Binding]
        public string CurrencyCode
        {
            get => UserSettingsModel.CurrencyCode;
            set
            {
                if(value == CurrencyCode) return;
                UserSettingsModel.CurrencyCode = value;
                SyncChangedPropertyWithServer(value, MainNames.ModelsPropertiesNames.Currency);
            }
        }


        [Binding]
        public bool ShowAdsState
        {
            get => UserSettingsModel.ShowAdsState;
            set
            {
                if(value == ShowAdsState) return;
                UserSettingsModel.ShowAdsState = value;
                SyncChangedPropertyWithServer(PropertiesUtility.BoolToString(value), MainNames.ModelsPropertiesNames.ShowAds);
            }
        }

        [Binding]
        public bool ShowAlertsState
        {
            get => UserSettingsModel.ShowAlertsState;
            set
            {
                if(value == ShowAlertsState) return;
                UserSettingsModel.ShowAlertsState = value;
                SyncChangedPropertyWithServer(PropertiesUtility.BoolToString(value), MainNames.ModelsPropertiesNames.ShowAlerts);
            }
        }

        [Binding]
        public bool UserRadarState
        {
            get => UserSettingsModel.UserRadarState;
            set
            {
                if(value == UserRadarState) return;
                UserSettingsModel.UserRadarState = value;
                SyncChangedPropertyWithServer(PropertiesUtility.BoolToString(value), MainNames.ModelsPropertiesNames.UserRadar);
            }
        }

        [Binding]
        public bool ShowNotificationsState
        {
            get => UserSettingsModel.ShowNotificationsState;
            set
            {
                if(value == ShowNotificationsState) return;
                UserSettingsModel.ShowNotificationsState = value;
                SyncChangedPropertyWithServer(PropertiesUtility.BoolToString(value), MainNames.ModelsPropertiesNames.ShowNotifications);
            }
        }


        public UserProfileViewModel() : base(nameof(UserProfileViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await repository.LoadDataFromServer().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public void CountryDropdown_OnClick()
        {
            SwitchToView(new ViewsPairInfo(nameof(SettingsView), nameof(CountrySelectionView)),
                new FormsTransitionBundle(new Action<int>(SetSelectedCountryIndex)));
        }

        [Binding]
        public void CurrencyDropdown_OnClick()
        {
            SwitchToView(new ViewsPairInfo(nameof(SettingsView), nameof(CurrencySelectionView)),
                new FormsTransitionBundle(new Action<int>(SetSelectedCurrencyIndex)));
        }

        private void SetSelectedCountryIndex(int index)
        {
            SelectedCountryIndex = index;
        }

        private void SetSelectedCurrencyIndex(int index)
        {
            SelectedCurrencyIndex = index;
        }

        [Binding]
        public void UserRadar_OnTryToTurnOn()
        {
            geoLocationRepository.SetLocationServiceActivity(!UserRadarState);
        }

        private async void SyncChangedPropertyWithServer(string value, string propertyName)
        {
            try
            {
                await ProfileDataStaticRequestsProcessor.UpdateUserProfileData(OperationCancellationController.CancellationToken,
                    userAuthorisationDataRepository, new KeyValuePair<string, string>(propertyName, value)).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => repository.PropertyChanged += value;
            remove => repository.PropertyChanged -= value;
        }
    }
}