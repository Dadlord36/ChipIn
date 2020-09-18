using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Structures;
using GlobalVariables;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class AccessViewModel : ViewsSwitchingViewModel, IMerchantProfileSettings, INotifyPropertyChanged
    {
        [SerializeField] private MerchantProfileSettingsRepository merchantProfileSettingsRepository;
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;

        private IMerchantProfileSettings MerchantProfileSettingsModelImplementation => merchantProfileSettingsRepository;


        #region IMerchantProfileSettingsModel implementation

        [Binding]
        public string Name
        {
            get => MerchantProfileSettingsModelImplementation.Name;
            set => MerchantProfileSettingsModelImplementation.Name = value;
        }

        [Binding]
        public string Email
        {
            get => MerchantProfileSettingsModelImplementation.Email;
            set => MerchantProfileSettingsModelImplementation.Email = value;
        }

        [Binding]
        public int UserId
        {
            get => MerchantProfileSettingsModelImplementation.Id ?? 0;
            set => MerchantProfileSettingsModelImplementation.Id = value;
        }

        public int? Id
        {
            get => MerchantProfileSettingsModelImplementation.Id;
            set => MerchantProfileSettingsModelImplementation.Id = value;
        }

        public string Role
        {
            get => MerchantProfileSettingsModelImplementation.Role;
            set => MerchantProfileSettingsModelImplementation.Role = value;
        }

        public string Gender
        {
            get => MerchantProfileSettingsModelImplementation.Gender;
            set => MerchantProfileSettingsModelImplementation.Gender = value;
        }

        public string Birthday
        {
            get => MerchantProfileSettingsModelImplementation.Birthday;
            set => MerchantProfileSettingsModelImplementation.Birthday = value;
        }


        public string CountryCode
        {
            get => MerchantProfileSettingsModelImplementation.CountryCode;
            set => MerchantProfileSettingsModelImplementation.CountryCode = value;
        }


        public string CurrencyCode
        {
            get => MerchantProfileSettingsModelImplementation.CurrencyCode;
            set => MerchantProfileSettingsModelImplementation.CurrencyCode = value;
        }


        public int TokensBalance
        {
            get => MerchantProfileSettingsModelImplementation.TokensBalance;
            set => MerchantProfileSettingsModelImplementation.TokensBalance = value;
        }

        public bool ShowAdsState
        {
            get => MerchantProfileSettingsModelImplementation.ShowAdsState;
            set
            {
                if (ShowAdsState == value) return;
                MerchantProfileSettingsModelImplementation.ShowAdsState = value;
                SyncChangedPropertyWithServer(ShowAdsState, MainNames.ModelsPropertiesNames.ShowAds);
            }
        }

        [Binding]
        public bool ShowAlertsState
        {
            get => MerchantProfileSettingsModelImplementation.ShowAlertsState;
            set
            {
                if (ShowAlertsState == value) return;
                MerchantProfileSettingsModelImplementation.ShowAlertsState = value;
                SyncChangedPropertyWithServer(ShowAlertsState, MainNames.ModelsPropertiesNames.ShowAlerts);
            }
        }

        [Binding]
        public bool ShowNotificationsState
        {
            get => MerchantProfileSettingsModelImplementation.ShowNotificationsState;
            set
            {
                if (ShowNotificationsState == value) return;
                MerchantProfileSettingsModelImplementation.ShowNotificationsState = value;
                SyncChangedPropertyWithServer(ShowNotificationsState, MainNames.ModelsPropertiesNames.ShowNotifications);
            }
        }


        public GeoLocation UserLocation
        {
            get => MerchantProfileSettingsModelImplementation.UserLocation;
            set => MerchantProfileSettingsModelImplementation.UserLocation = value;
        }

        [Binding]
        public string Avatar
        {
            get => MerchantProfileSettingsModelImplementation.Avatar;
            set => MerchantProfileSettingsModelImplementation.Avatar = value;
        }


        public bool UserRadarState
        {
            get => MerchantProfileSettingsModelImplementation.UserRadarState;
            set => MerchantProfileSettingsModelImplementation.UserRadarState = value;
        }

        [Binding]
        public string CompanyName
        {
            get => MerchantProfileSettingsModelImplementation.CompanyName;
            set => MerchantProfileSettingsModelImplementation.CompanyName = value;
        }

        [Binding]
        public string CompanyEmail
        {
            get => MerchantProfileSettingsModelImplementation.CompanyEmail;
            set => MerchantProfileSettingsModelImplementation.CompanyEmail = value;
        }

        [Binding]
        public string Slogan
        {
            get => MerchantProfileSettingsModelImplementation.Slogan;
            set => MerchantProfileSettingsModelImplementation.Slogan = value;
        }

        [Binding]
        public Sprite AvatarSprite
        {
            get => MerchantProfileSettingsModelImplementation.AvatarSprite;
            set => MerchantProfileSettingsModelImplementation.AvatarSprite = value;
        }

        [Binding]
        public Sprite LogoSprite
        {
            get => MerchantProfileSettingsModelImplementation.LogoSprite;
            set => MerchantProfileSettingsModelImplementation.LogoSprite = value;
        }

        public string FirstName
        {
            get => merchantProfileSettingsRepository.FirstName;
            set => merchantProfileSettingsRepository.FirstName = value;
        }

        public string LastName
        {
            get => merchantProfileSettingsRepository.LastName;
            set => merchantProfileSettingsRepository.LastName = value;
        }

        [Binding]
        public bool SetReminderSAdCAdExpiring
        {
            get => MerchantProfileSettingsModelImplementation.SetReminderSAdCAdExpiring;
            set
            {
                if (SetReminderSAdCAdExpiring == value) return;
                MerchantProfileSettingsModelImplementation.SetReminderSAdCAdExpiring = value;
                SyncChangedPropertyWithServer(SetReminderSAdCAdExpiring, MainNames.ModelsPropertiesNames.SetReminderSAdCAdExpiring);
            }
        }

        public string LogoUrl
        {
            get => merchantProfileSettingsRepository.LogoUrl;
            set => merchantProfileSettingsRepository.LogoUrl = value;
        }

        #endregion

        public AccessViewModel() : base(nameof(AccessViewModel))
        {
        }

        [Binding]
        public void ChangePasswordButton_OnClick()
        {
        }

        [Binding]
        public void EditButton_OnClick()
        {
            SwitchToView(nameof(EditAdminView));
        }


        private async void SyncChangedPropertyWithServer<T>(T value, string propertyName) where T : struct
        {
            try
            {
                await ProfileDataStaticRequestsProcessor.UpdateUserProfileData(OperationCancellationController.CancellationToken,
                        userAuthorisationDataRepository, new KeyValuePair<string, string>(propertyName, PropertiesUtility.ToString(value)))
                    .ConfigureAwait(false);
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
            add => merchantProfileSettingsRepository.PropertyChanged += value;
            remove => merchantProfileSettingsRepository.PropertyChanged -= value;
        }
    }
}