using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using DataModels;
using JetBrains.Annotations;
using UnityWeld.Binding;
using ViewModels.Interfaces;

namespace ViewModels
{
    [Binding]
    public class RegistrationViewModel : BaseViewModel, IUserRegistrationModel, INotifyPropertyChanged
    {
        public event Action<UserRegistrationModel> OnTryToRegister;
        private IRegistrationListener _registrationListener;
        
        private readonly UserRegistrationModel _registrationModel = new UserRegistrationModel();
        private bool _pendingRegister;

        [Binding]
        public MailAddress Email
        {
            get => _registrationModel.Email;
            set
            {
                _registrationModel.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        [Binding]
        public string Password
        {
            get => _registrationModel.Password;
            set
            {
                _registrationModel.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        [Binding]
        public string Gender
        {
            get => _registrationModel.Gender;
            set
            {
                _registrationModel.Gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        [Binding]
        public string Role
        {
            get => _registrationModel.Role;
            set
            {
                _registrationModel.Role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public bool PendingRegister
        {
            get => _pendingRegister;
            set
            {
                _pendingRegister = value;
                OnPropertyChanged(nameof(PendingRegister));
            }
        }

        public void TryToRegister()
        {
            _registrationListener.RequestStarted();
            PendingRegister = true;
            if (OnTryToRegister == null) return;
            OnTryToRegister?.Invoke(_registrationModel);
            PendingRegister = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}