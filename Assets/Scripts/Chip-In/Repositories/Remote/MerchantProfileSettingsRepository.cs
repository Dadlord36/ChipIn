using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Structures;
using DataModels.Extensions;
using DataModels.Interfaces;
using GlobalVariables;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Repositories.Local;
using RequestsStaticProcessors;
using Tasking;
using UnityEngine;
using Utilities;

namespace Repositories.Remote
{
    public interface ISetReminderSAdCadExpiring
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.SetReminderSAdCAdExpiring)]
        bool SetReminderSAdCAdExpiring { get; set; }
    }

    public interface ICompanyName
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.CompanyName)]
        string CompanyName { get; set; }
    }

    public interface ICompanyEmail
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.CompanyEmail)]
        string CompanyEmail { get; set; }
    }

    public interface IMerchantProfileSettingsModel : IUserProfileModel, ICompanyName, ICompanyEmail, ISlogan, ISetReminderSAdCadExpiring,
        ILogoImageUrl
    {
    }

    public class MerchantProfileSettingsDataModel : IMerchantProfileSettingsModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Id { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public int TokensBalance { get; set; }
        public bool ShowAdsState { get; set; }
        public bool ShowAlertsState { get; set; }
        public bool ShowNotificationsState { get; set; }
        public GeoLocation UserLocation { get; set; }
        public string Avatar { get; set; }
        public bool UserRadarState { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string Slogan { get; set; }
        public bool SetReminderSAdCAdExpiring { get; set; }
        public string LogoUrl { get; set; }
    }

    public interface IMerchantProfileSettings : IMerchantProfileSettingsModel
    {
        Sprite AvatarSprite { get; set; }
        Sprite LogoSprite { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    [CreateAssetMenu(fileName = nameof(MerchantProfileSettingsRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(MerchantProfileSettingsRepository), order = 0)]
    public sealed class MerchantProfileSettingsRepository : RemoteRepositoryBase, INotifyPropertyChanged, IMerchantProfileSettings
    {
        private const string Tag = nameof(MerchantProfileSettingsRepository);

        [SerializeField] private UserAuthorisationDataRepository userAuthorisationDataRepository;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;

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
        private Sprite _avatarSprite;
        private Sprite _logoSprite;


        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                {
                    var parts = _name.Split(' ');

                    if (parts.Length > 0)
                    {
                        FirstName = parts[0];
                    }

                    if (parts.Length > 1)
                    {
                        LastName = parts[1];
                    }
                }
               
                OnPropertyChanged();
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
            }
        }

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

        public bool ShowAdsState
        {
            get => _showAdsState;
            set
            {
                if (value == _showAdsState) return;
                _showAdsState = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAlertsState
        {
            get => _showAlertsState;
            set
            {
                if (value == _showAlertsState) return;
                _showAlertsState = value;
                OnPropertyChanged();
            }
        }

        public bool ShowNotificationsState
        {
            get => _showNotificationsState;
            set
            {
                if (value == _showNotificationsState) return;
                _showNotificationsState = value;
                OnPropertyChanged();
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

        public Sprite AvatarSprite
        {
            get => _avatarSprite;
            set
            {
                _avatarSprite = value;
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

        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (value == _companyName) return;
                _companyName = value;
                OnPropertyChanged();
            }
        }

        public string CompanyEmail
        {
            get => _companyEmail;
            set
            {
                if (value == _companyEmail) return;
                _companyEmail = value;
                OnPropertyChanged();
            }
        }

        public string Slogan
        {
            get => _slogan;
            set
            {
                if (value == _slogan) return;
                _slogan = value;
                OnPropertyChanged();
            }
        }

        public Sprite LogoSprite
        {
            get => _logoSprite;
            set
            {
                _logoSprite = value;
                OnPropertyChanged();
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

        public bool SetReminderSAdCAdExpiring
        {
            get => _setReminderSAdCAdExpiring;
            set
            {
                if (value == _setReminderSAdCAdExpiring) return;
                _setReminderSAdCAdExpiring = value;
                OnPropertyChanged();
            }
        }

        public string FirstName {  set; get; }
        public string LastName {  set; get; }

        public override async Task LoadDataFromServer()
        {
            CancelOngoingTask();
            if (userAuthorisationDataRepository.UserRole != MainNames.UserRoles.BusinessOwner) return;
            try
            {
                var response = await ProfileDataStaticRequestsProcessor.GetMerchantProfileData(out _, userAuthorisationDataRepository);

                if (!response.Success) return;

                var responseInterface = response.ResponseModelInterface;
                this.Set(responseInterface.User);

                if (!string.IsNullOrEmpty(LogoUrl))
                    LogoSprite = await downloadedSpritesRepository.CreateLoadSpriteTask(LogoUrl, TasksCancellationTokenSource.Token);
                if (!string.IsNullOrEmpty(Avatar))
                    AvatarSprite = await downloadedSpritesRepository.CreateLoadSpriteTask(Avatar, TasksCancellationTokenSource.Token);
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
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}