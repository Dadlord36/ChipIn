using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using DataModels.RequestsModels;
using JetBrains.Annotations;
using RequestsStaticProcessors;
using ScriptableObjects.Validations;
using UnityEngine;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class RegistrationViewModel : ViewsSwitchingViewModel, IBasicLoginModel,
        INotifyPropertyChanged
    {
        private readonly SimpleRegistrationRequestModel
            _registrationRequestModel = new SimpleRegistrationRequestModel();

        [SerializeField] private UserSimpleRegisterModelValidator userSimpleRegisterModelValidator;


        private bool _pendingRegister;
        private bool _canTryRegister;
        private string _repeatedPassword;

        [Binding]
        public string Email
        {
            get => _registrationRequestModel.Email;
            set
            {
                _registrationRequestModel.Email = value;
                OnPropertyChanged();
                CheckIfCanRegister();
            }
        }

        [Binding]
        public string Password
        {
            get => _registrationRequestModel.Password;
            set
            {
                _registrationRequestModel.Password = value;
                OnPropertyChanged();
                CheckIfCanRegister();
            }
        }

        [Binding]
        public bool CanTryRegister
        {
            get => _canTryRegister;
            private set
            {
                if (value == _canTryRegister) return;
                _canTryRegister = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool PendingRegister
        {
            get => _pendingRegister;
            set
            {
                _pendingRegister = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string RepeatedPassword
        {
            get => _repeatedPassword;
            set
            {
                if (value == _repeatedPassword) return;
                _repeatedPassword = value;
                OnPropertyChanged();
                CheckIfCanRegister();
            }
        }

        [Binding]
        public async void TryToRegister()
        {
            PendingRegister = true;
            await Register();
            PendingRegister = false;
        }

        [Binding]
        public void SwitchToLoginView()
        {
            SwitchToView(nameof(LoginView));
        }

        private void SwitchToCheckYourEmailView()
        {
            SwitchToView(nameof(CheckEmailView));
        }

        private void CheckIfCanRegister()
        {
            if (string.IsNullOrEmpty(_repeatedPassword)) return;

            bool CheckPasswordsAreMatch()
            {
                return Password == RepeatedPassword;
            }

            CanTryRegister = userSimpleRegisterModelValidator.CheckIsValid(_registrationRequestModel) &&
                             CheckPasswordsAreMatch();
        }

        private async Task Register()
        {
            // If registration was successful 
            if (await RegistrationStaticProcessor.RegisterUserSimple(_registrationRequestModel))
            {
                Debug.Log("User have been registered successfully!");
                SwitchToCheckYourEmailView();
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