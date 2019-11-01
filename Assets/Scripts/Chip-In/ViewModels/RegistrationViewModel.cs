using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels;
using HttpRequests.RequestsProcessors;
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
        public event Action RegistrationStarted;
        public event Action<string> RegistrationFailed;
        public event Action<UserProfileModel> RegistrationSuccessfullyComplete;

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

/*        [Binding]
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
        }*/

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
        public void TryToRegister()
        {
            RegistrationStarted?.Invoke();
            Register();
        }

        private void CheckIfCanRegister()
        {
            if(string.IsNullOrEmpty(_repeatedPassword) ) return;
            
            bool CheckPasswordsAreMatch()
            {
                return Password == RepeatedPassword;
            }

            CanTryRegister = userSimpleRegisterModelValidator.CheckIsValid(_registrationModel) &&
                             CheckPasswordsAreMatch();
        }

        private async void Register()
        {
            PendingRegister = true;
            try
            {
                var response = await new RegistrationRequestProcessor().SendRequest(_registrationModel);
                if (response.responseData == null)
                {
                    RegistrationFailed?.Invoke(response.responseMessage.ReasonPhrase);
                }

                RegistrationSuccessfullyComplete?.Invoke(response.responseData);
                if (response.responseMessage.IsSuccessStatusCode)
                {
                    PendingRegister = false;
                }
            }
            catch (ApiException e)
            {
                RegistrationFailed?.Invoke(e.Message);
                PendingRegister = false;
                throw;
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