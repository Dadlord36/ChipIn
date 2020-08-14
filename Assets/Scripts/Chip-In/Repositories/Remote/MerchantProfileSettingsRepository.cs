using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

    public interface IMerchantProfileSettingsModel : IUserProfile
    {
        string PersonInChargeName { get; set; }
        bool SetReminderSAdCAdExpiring { get; set; }
        bool ShowAlerts { get; set; }
        bool ShowNotifications { get; set; }
        string BranchCode1 { get; set; }
        string BranchCode2 { get; set; }
        string BranchCode3 { get; set; }
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
        public string BranchCode1 { get; set; }
        public string BranchCode2 { get; set; }
        public string BranchCode3 { get; set; }
    }

    [CreateAssetMenu(fileName = nameof(MerchantProfileSettingsRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(MerchantProfileSettingsRepository), order = 0)]
    public sealed class MerchantProfileSettingsRepository : RemoteRepositoryBase, IMerchantProfileSettingsModel,
        INotifyPropertyChanged
    {
        private readonly MerchantProfileSettingsDataModel _merchantProfileSettingsData =
            new MerchantProfileSettingsDataModel
            {
                BranchCode1 = "2361", BranchCode2 = "032", BranchCode3 = "6214", Name = "jhon Gremm",
                Email = "gremm@gmail.com", PersonInChargeName = "maria smith"
            };

        private IMerchantProfileSettingsModel MerchantProfileSettingsModel => _merchantProfileSettingsData;

        public override async Task LoadDataFromServer()
        {
            throw new NotImplementedException();
        }


        public string Name
        {
            get => MerchantProfileSettingsModel.Name;
            set
            {
                MerchantProfileSettingsModel.Name = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => MerchantProfileSettingsModel.Id;
            set
            {
                MerchantProfileSettingsModel.Id = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => MerchantProfileSettingsModel.Email;
            set
            {
                MerchantProfileSettingsModel.Email = value;
                OnPropertyChanged();
            }
        }

        public string PersonInChargeName
        {
            get => MerchantProfileSettingsModel.PersonInChargeName;
            set
            {
                MerchantProfileSettingsModel.PersonInChargeName = value;
                OnPropertyChanged();
            }
        }

        public bool SetReminderSAdCAdExpiring
        {
            get => MerchantProfileSettingsModel.SetReminderSAdCAdExpiring;
            set
            {
                MerchantProfileSettingsModel.SetReminderSAdCAdExpiring = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAlerts
        {
            get => MerchantProfileSettingsModel.ShowAlerts;
            set
            {
                MerchantProfileSettingsModel.ShowAlerts = value;
                OnPropertyChanged();
            }
        }

        public bool ShowNotifications
        {
            get => MerchantProfileSettingsModel.ShowNotifications;
            set
            {
                MerchantProfileSettingsModel.ShowNotifications = value;
                OnPropertyChanged();
            }
        }

        public string BranchCode1
        {
            get => MerchantProfileSettingsModel.BranchCode1;
            set
            {
                MerchantProfileSettingsModel.BranchCode1 = value;
                OnPropertyChanged();
            }
        }

        public string BranchCode2
        {
            get => MerchantProfileSettingsModel.BranchCode2;
            set
            {
                MerchantProfileSettingsModel.BranchCode2 = value;
                OnPropertyChanged();
            }
        }

        public string BranchCode3
        {
            get => MerchantProfileSettingsModel.BranchCode3;
            set
            {
                MerchantProfileSettingsModel.BranchCode3 = value;
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