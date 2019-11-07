using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using HttpRequests.RequestsProcessors;
using HumbleObjects;
using JetBrains.Annotations;
using ScriptableObjects.Validations;
using UnityEngine;
using UnityWeld.Binding;
using Utilities.ApiExceptions;

namespace ViewModels
{
    [Binding]
    public class RegistrationViewModel : BaseViewModel, IUserSimpleRegistrationModel, INotifyPropertyChanged
    {
        private readonly UserSimpleRegistrationModel _registrationModel = new UserSimpleRegistrationModel();
        [SerializeField] private UserSimpleRegisterModelValidator userSimpleRegisterModelValidator;


        private bool _pendingRegister;
        private bool _canTryRegister;
        private string _repeatedPassword;

        [Binding]
        public string Email
        {
            get => _registrationModel.Email;
            set
            {
                _registrationModel.Email = value;
                OnPropertyChanged(nameof(Email));
                CheckIfCanRegister();
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
                OnPropertyChanged(nameof(CanTryRegister));
            }
        }

        [Binding]
        public bool PendingRegister
        {
            get => _pendingRegister;
            set
            {
                _pendingRegister = value;
                OnPropertyChanged(nameof(PendingRegister));
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
                OnPropertyChanged(nameof(RepeatedPassword));
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
            viewsSwitchingBinding.SwitchView<LoginViewModel>(View);
        }

        private void SwitchToCheckYourEmailView()
        {
            viewsSwitchingBinding.SwitchView<CheckEmailViewModel>(View);
        }

        private void CheckIfCanRegister()
        {
            if (string.IsNullOrEmpty(_repeatedPassword)) return;

            bool CheckPasswordsAreMatch()
            {
                return Password == RepeatedPassword;
            }

            CanTryRegister = userSimpleRegisterModelValidator.CheckIsValid(_registrationModel) &&
                             CheckPasswordsAreMatch();
        }

        private async Task Register()
        {
            // If registration was successful 
            if (await RegistrationProcessor.RegisterUserSimple(_registrationModel))
            {
                Debug.Log("User have been registered successfully!");
                SwitchToCheckYourEmailView();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}