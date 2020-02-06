using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Elements
{
    [Binding]
    public sealed class PasswordChangingViewModel : BaseViewModel,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        [SerializeField] private UserProfileRemoteRepository userProfileRemoteRepository;
        [SerializeField] private PasswordAnalyzer passwordAnalyzer;
        
        private bool _canChangePassword;

        [Binding]
        public string Password    
        {
            get => passwordAnalyzer.OriginalPassword;
            set
            {
                passwordAnalyzer.OriginalPassword = value;
                CheckIfCanConfirmChange();
                OnPropertyChanged();
            }
        }

        [Binding]
        public string PasswordRepeat
        {
            get => passwordAnalyzer.RepeatedPassword;
            set
            {
                passwordAnalyzer.RepeatedPassword = value;
                CheckIfCanConfirmChange();
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool CanChangePassword
        {
            get => _canChangePassword;
            set
            {
                if (value == _canChangePassword) return;
                _canChangePassword = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public void Confirm_OnClick()
        {
            View.Hide();
        }

        private void CheckIfCanConfirmChange()
        {
            CanChangePassword = passwordAnalyzer.CheckIfPasswordsAreMatchAndItIsValid();
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}