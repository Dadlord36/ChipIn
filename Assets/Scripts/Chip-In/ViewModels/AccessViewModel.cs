using System.ComponentModel;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public sealed class AccessViewModel : ViewsSwitchingViewModel, IMerchantProfileSettingsModel, INotifyPropertyChanged
    {
        [SerializeField] private MerchantProfileSettingsRepository merchantProfileSettingsRepository;
        private IMerchantProfileSettingsModel MerchantProfileSettingsModelImplementation => merchantProfileSettingsRepository;


        [Binding]
        public string Name
        {
            get => MerchantProfileSettingsModelImplementation.Name;
            set => MerchantProfileSettingsModelImplementation.Name = value;
        }

        [Binding]
        public int Id
        {
            get => MerchantProfileSettingsModelImplementation.Id;
            set => MerchantProfileSettingsModelImplementation.Id = value;
        }

        [Binding]
        public string Email
        {
            get => MerchantProfileSettingsModelImplementation.Email;
            set => MerchantProfileSettingsModelImplementation.Email = value;
        }

        [Binding]
        public string PersonInChargeName
        {
            get => MerchantProfileSettingsModelImplementation.PersonInChargeName;
            set => MerchantProfileSettingsModelImplementation.PersonInChargeName = value;
        }

        [Binding]
        public bool SetReminderSAdCAdExpiring
        {
            get => MerchantProfileSettingsModelImplementation.SetReminderSAdCAdExpiring;
            set => MerchantProfileSettingsModelImplementation.SetReminderSAdCAdExpiring = value;
        }

        [Binding]
        public bool ShowAlerts
        {
            get => MerchantProfileSettingsModelImplementation.ShowAlerts;
            set => MerchantProfileSettingsModelImplementation.ShowAlerts = value;
        }

        [Binding]
        public bool ShowNotifications
        {
            get => MerchantProfileSettingsModelImplementation.ShowNotifications;
            set => MerchantProfileSettingsModelImplementation.ShowNotifications = value;
        }

        [Binding]
        public string BranchCode1
        {
            get => MerchantProfileSettingsModelImplementation.BranchCode1;
            set => MerchantProfileSettingsModelImplementation.BranchCode1 = value;
        }

        [Binding]
        public string BranchCode2
        {
            get => MerchantProfileSettingsModelImplementation.BranchCode2;
            set => MerchantProfileSettingsModelImplementation.BranchCode2 = value;
        }

        [Binding]
        public string BranchCode3
        {
            get => MerchantProfileSettingsModelImplementation.BranchCode3;
            set => MerchantProfileSettingsModelImplementation.BranchCode3 = value;
        }
        
        public AccessViewModel() : base(nameof(AccessViewModel))
        {
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => merchantProfileSettingsRepository.PropertyChanged += value;
            remove => merchantProfileSettingsRepository.PropertyChanged -= value;
        }
    }
}