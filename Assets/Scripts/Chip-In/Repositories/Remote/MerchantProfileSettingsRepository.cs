using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Repositories.Remote
{
    public interface IUserProfile
    {
        string Name { get; set; }
        int Id { get; set; }
        string Email { get; set; }
    }

    public interface IMerchantProfileSettingsModel : IUserProfile, ISlogan
    {
        string PersonInChargeName { get; set; }
        bool SetReminderSAdCAdExpiring { get; set; }
        bool ShowAlerts { get; set; }
        bool ShowNotifications { get; set; }
    }

    public class MerchantProfileSettingsDataModel : IMerchantProfileSettingsModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string PersonInChargeName { get; set; }
        public bool SetReminderSAdCAdExpiring { get; set; }
        public bool ShowAlerts { get; set; }
        public bool ShowNotifications { get; set; }
        public string Slogan { get; set; }
    }

    [CreateAssetMenu(fileName = nameof(MerchantProfileSettingsRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(MerchantProfileSettingsRepository), order = 0)]
    public sealed class MerchantProfileSettingsRepository : RemoteRepositoryBase, IMerchantProfileSettingsModel,
        INotifyPropertyChanged
    {
        private readonly IMerchantProfileSettingsModel _merchantProfileSettingsModel =
            new MerchantProfileSettingsDataModel
            {
                Name = "jhon Gremm", Email = "gremm@gmail.com", PersonInChargeName = "maria smith", Slogan = "Some Slogan"
            };

        public override async Task LoadDataFromServer()
        {
            throw new NotImplementedException();
        }


        public string Name
        {
            get => _merchantProfileSettingsModel.Name;
            set
            {
                _merchantProfileSettingsModel.Name = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => _merchantProfileSettingsModel.Id;
            set
            {
                _merchantProfileSettingsModel.Id = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _merchantProfileSettingsModel.Email;
            set
            {
                _merchantProfileSettingsModel.Email = value;
                OnPropertyChanged();
            }
        }

        public string PersonInChargeName
        {
            get => _merchantProfileSettingsModel.PersonInChargeName;
            set
            {
                _merchantProfileSettingsModel.PersonInChargeName = value;
                OnPropertyChanged();
            }
        }

        public bool SetReminderSAdCAdExpiring
        {
            get => _merchantProfileSettingsModel.SetReminderSAdCAdExpiring;
            set
            {
                _merchantProfileSettingsModel.SetReminderSAdCAdExpiring = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAlerts
        {
            get => _merchantProfileSettingsModel.ShowAlerts;
            set
            {
                _merchantProfileSettingsModel.ShowAlerts = value;
                OnPropertyChanged();
            }
        }

        public bool ShowNotifications
        {
            get => _merchantProfileSettingsModel.ShowNotifications;
            set
            {
                _merchantProfileSettingsModel.ShowNotifications = value;
                OnPropertyChanged();
            }
        }

        public string Slogan
        {
            get => _merchantProfileSettingsModel.Slogan;
            set
            {
                _merchantProfileSettingsModel.Slogan = value;
                OnPropertyChanged();
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