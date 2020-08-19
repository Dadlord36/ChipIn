using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Controllers;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;
using Views.OptionsSelectionViews;

namespace ViewModels
{
    [Binding]
    public sealed class SettingsViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private SessionController sessionController;

        private int _selectedCountryIndex;
        private int _selectedCurrencyIndex;

        public SettingsViewModel() : base(nameof(SettingsViewModel))
        {
        }

        [Binding]
        public int SelectedCountryIndex
        {
            get => _selectedCountryIndex;
            set
            {
                if (value == _selectedCountryIndex) return;
                _selectedCountryIndex = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int SelectedCurrencyIndex
        {
            get => _selectedCurrencyIndex;
            set
            {
                if (value == _selectedCurrencyIndex) return;
                _selectedCurrencyIndex = value;
                OnPropertyChanged();
            }
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
        public async void LogOut_OnClick()
        {
            try
            {
                await LogOut().ConfigureAwait(true);
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

        [Binding]
        public void EditProfileButton_OnClick()
        {
            SwitchToView(nameof(EditProfileView));
        }
        
        
        [Binding]
        public void CountryDropdown_OnClick()
        {
            SwitchToView(nameof(CountrySelectionView), new FormsTransitionBundle(new Action<int>(SetSelectedCountryIndex)));
        }

        [Binding]
        public void CurrencyDropdown_OnClick()
        {
            SwitchToView(nameof(CurrencySelectionView), new FormsTransitionBundle(new Action<int>(SetSelectedCurrencyIndex)));
        }

        [Binding]
        public void ShowMyWallet()
        {
           
        }

        [Binding]
        public void ShowMyInterest()
        {
            
        }

        private Task LogOut()
        {
            return sessionController.SignOut();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}