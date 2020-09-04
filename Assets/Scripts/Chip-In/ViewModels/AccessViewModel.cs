using System.ComponentModel;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Views;

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
        public string Slogan
        {
            get => merchantProfileSettingsRepository.Slogan;
            set => merchantProfileSettingsRepository.Slogan = value;
        }
        
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

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => merchantProfileSettingsRepository.PropertyChanged += value;
            remove => merchantProfileSettingsRepository.PropertyChanged -= value;
        }


    }
}