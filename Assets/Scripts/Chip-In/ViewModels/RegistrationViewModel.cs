using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using JetBrains.Annotations;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class RegistrationViewModel : ViewsSwitchingViewModel, IBasicLoginModel,
        INotifyPropertyChanged
    {
        private const string Tag = nameof(RegistrationViewModel);

        private readonly RegistrationRequestModel
            _registrationRequestModel = new RegistrationRequestModel();

        [SerializeField] private PasswordAnalyzer passwordAnalyzer;
        [SerializeField] private ScriptableObjects.Validations.EmailValidation emailValidator;
        [SerializeField] private AlertCardController alertCardController;

        private bool _pendingRegister;
        private bool _canTryRegister;

        [Binding]
        public string Role
        {
            get => _registrationRequestModel.Role;
            set
            {
                if (_registrationRequestModel.Role == value) return;
                _registrationRequestModel.Role = value;
                OnPropertyChanged();
            }
        }

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
                passwordAnalyzer.OriginalPassword = value;
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
            get => passwordAnalyzer.RepeatedPassword;
            set
            {
                if (value == passwordAnalyzer.RepeatedPassword) return;
                passwordAnalyzer.RepeatedPassword = value;
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
            if (string.IsNullOrEmpty(passwordAnalyzer.RepeatedPassword)) return;

            CanTryRegister = emailValidator.CheckIsValid(_registrationRequestModel.Email) &&
                             passwordAnalyzer.CheckIfPasswordsAreMatchAndItIsValid();
        }

        private async Task Register()
        {
            var result = await RegistrationStaticProcessor.TryRegisterUserFull(out TasksCancellationTokenSource, _registrationRequestModel);
            // If registration was successful 
            if (result.Success)
            {
                LogUtility.PrintLog(Tag, "User have been registered successfully!");
                SwitchToCheckYourEmailView();
            }
            else
            {
                alertCardController.ShowAlertWithText(result.Error);
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