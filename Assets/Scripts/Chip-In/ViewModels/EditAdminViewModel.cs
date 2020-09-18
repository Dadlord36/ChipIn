using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common;
using Common.Structures;
using DataModels.Extensions;
using GlobalVariables;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using WebOperationUtilities;

namespace ViewModels
{
    [Binding]
    public sealed class EditAdminViewModel : ViewsSwitchingViewModel, IMerchantProfileSettings, INotifyPropertyChanged
    {
        [SerializeField] private MerchantProfileSettingsRepository merchantProfileSettingsRepository;
        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;

        private readonly ChangedPropertiesCollector changedPropertiesCollector = new ChangedPropertiesCollector();
        private string _newAvatarImagePath;
        private string _newLogoImagePath;
        private Sprite _avatarSprite;

        private Sprite _logoSprite;
        private string _name;
        private string _email;
        private int? _id;
        private string _role;
        private string _gender;
        private string _birthday;
        private string _countryCode;
        private string _currencyCode;
        private int _tokensBalance;
        private bool _showAdsState;
        private bool _showAlertsState;
        private bool _showNotificationsState;
        private GeoLocation _userLocation;
        private string _avatar;
        private bool _userRadarState;
        private string _companyName;
        private string _companyEmail;
        private string _slogan;
        private bool _setReminderSAdCAdExpiring;
        private string _logoUrl;
        private string _firstName;
        private string _lastName;
        
        
        [Binding]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                CombineFirstAndLastName();
                OnPropertyChanged();
            }
        }

        [Binding]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                CombineFirstAndLastName();
                OnPropertyChanged();
            }
        }

        [Binding]
        public string NewAvatarImagePath
        {
            get => _newAvatarImagePath;
            set
            {
                if (value == _newAvatarImagePath) return;
                _newAvatarImagePath = value;
                OnPropertyChanged();
                AvatarSprite = SpritesUtility.CreateSpriteWithDefaultParameters(NativeGallery.LoadImageAtPath(value));
                changedPropertiesCollector.AddChangedFieldWithFileData(value, MainNames.ModelsPropertiesNames.Avatar);
            }
        }

        [Binding]
        public string NewLogoImagePath
        {
            get => _newLogoImagePath;
            set
            {
                if (value == _newLogoImagePath) return;
                _newLogoImagePath = value;
                OnPropertyChanged();
                LogoSprite = SpritesUtility.CreateSpriteWithDefaultParameters(NativeGallery.LoadImageAtPath(value));
                changedPropertiesCollector.AddChangedFieldWithFileData(value, MainNames.ModelsPropertiesNames.Logo);
            }
        }

        [Binding]
        public Sprite AvatarSprite
        {
            get => _avatarSprite;
            set
            {
                _avatarSprite = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite LogoSprite
        {
            get => _logoSprite;
            set
            {
                _logoSprite = value;
                OnPropertyChanged();
            }
        }
        
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
                changedPropertiesCollector.AddChangedField(value, MainNames.ModelsPropertiesNames.Name);
            }
        }
        
        public string Email
        {
            get => _email;
            set
            {
                if (value == _email) return;
                _email = value;
                OnPropertyChanged();
                changedPropertiesCollector.AddChangedField(value, MainNames.ModelsPropertiesNames.Email);
            }
        }

        [Binding]
        public int? Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }


        public string Role
        {
            get => _role;
            set
            {
                if (value == _role) return;
                _role = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get => _gender;
            set
            {
                if (value == _gender) return;
                _gender = value;
                OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get => _birthday;
            set
            {
                if (value == _birthday) return;
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public string CountryCode
        {
            get => _countryCode;
            set
            {
                if (value == _countryCode) return;
                _countryCode = value;
                OnPropertyChanged();
            }
        }

        public string CurrencyCode
        {
            get => _currencyCode;
            set
            {
                if (value == _currencyCode) return;
                _currencyCode = value;
                OnPropertyChanged();
            }
        }

        public int TokensBalance
        {
            get => _tokensBalance;
            set
            {
                if (value == _tokensBalance) return;
                _tokensBalance = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool ShowAdsState
        {
            get => _showAdsState;
            set
            {
                if (value == _showAdsState) return;
                _showAdsState = value;
                OnPropertyChanged();
                changedPropertiesCollector.AddChangedField(PropertiesUtility.BoolToString(value), MainNames.ModelsPropertiesNames.ShowAds);
            }
        }

        [Binding]
        public bool ShowAlertsState
        {
            get => _showAlertsState;
            set
            {
                if (value == _showAlertsState) return;
                _showAlertsState = value;
                OnPropertyChanged();
                changedPropertiesCollector.AddChangedField(PropertiesUtility.BoolToString(value), MainNames.ModelsPropertiesNames.ShowAlerts);
            }
        }

        [Binding]
        public bool ShowNotificationsState
        {
            get => _showNotificationsState;
            set
            {
                if (value == _showNotificationsState) return;
                _showNotificationsState = value;
                OnPropertyChanged();
                changedPropertiesCollector.AddChangedField(PropertiesUtility.BoolToString(value), MainNames.ModelsPropertiesNames.ShowNotifications);
            }
        }

        public GeoLocation UserLocation
        {
            get => _userLocation;
            set
            {
                if (Equals(value, _userLocation)) return;
                _userLocation = value;
                OnPropertyChanged();
            }
        }

        public string Avatar
        {
            get => _avatar;
            set
            {
                if (value == _avatar) return;
                _avatar = value;
                OnPropertyChanged();
            }
        }

        public bool UserRadarState
        {
            get => _userRadarState;
            set
            {
                if (value == _userRadarState) return;
                _userRadarState = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (value == _companyName) return;
                _companyName = value;
                OnPropertyChanged();
                changedPropertiesCollector.AddChangedField(value, MainNames.ModelsPropertiesNames.CompanyName);
            }
        }

        [Binding]
        public string CompanyEmail
        {
            get => _companyEmail;
            set
            {
                if (value == _companyEmail) return;
                _companyEmail = value;
                OnPropertyChanged();
                changedPropertiesCollector.AddChangedField(value, MainNames.ModelsPropertiesNames.CompanyEmail);
            }
        }

        [Binding]
        public string Slogan
        {
            get => _slogan;
            set
            {
                if (value == _slogan) return;
                _slogan = value;
                OnPropertyChanged();
                changedPropertiesCollector.AddChangedField(value, MainNames.ModelsPropertiesNames.Slogan);
            }
        }

        [Binding]
        public bool SetReminderSAdCAdExpiring
        {
            get => _setReminderSAdCAdExpiring;
            set
            {
                if (value == _setReminderSAdCAdExpiring) return;
                _setReminderSAdCAdExpiring = value;
                OnPropertyChanged();
                changedPropertiesCollector.AddChangedField(PropertiesUtility.BoolToString(value),
                    MainNames.ModelsPropertiesNames.SetReminderSAdCAdExpiring);
            }
        }

        public string LogoUrl
        {
            get => _logoUrl;
            set
            {
                if (value == _logoUrl) return;
                _logoUrl = value;
                OnPropertyChanged();
            }
        }


        public EditAdminViewModel() : base(nameof(EditAdminViewModel))
        {
        }


        [Binding]
        public async void ConfirmButton_OnClick()
        {
            try
            {
                IsAwaitingProcess = true;
                await UpdateProfileData().ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            finally
            {
                IsAwaitingProcess = false;
            }
        }
        
        private void CombineFirstAndLastName()
        {
            Name = $"{FirstName} {LastName}";
        }
        
        
        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            this.Set(merchantProfileSettingsRepository);
            changedPropertiesCollector.ClearCollectedFields();
        }


        private async Task UpdateProfileData()
        {
            var response = await ProfileDataStaticRequestsProcessor.UpdateUserProfileData(OperationCancellationController.CancellationToken,
                userAuthorisationDataRepository, changedPropertiesCollector).ConfigureAwait(false);

            if (response.IsSuccessful)
            {
                changedPropertiesCollector.ClearCollectedFields();
                await merchantProfileSettingsRepository.LoadDataFromServer().ConfigureAwait(false);
            }
            
            alertCardController.ShowAlertWithText(response.IsSuccessful ? "Profile data was update successfully" : "Failed to update profile data");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}