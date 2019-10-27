using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using DataModels;
using HttpRequests;
using JetBrains.Annotations;
using UnityWeld.Binding;
using Utilities.ApiExceptions;

namespace ViewModels
{
    [Binding]
    public class RegistrationViewModel : BaseViewModel, IUserRegistrationModel, INotifyPropertyChanged
    {
        public event Action RegistrationStarted;
        public event Action<string> RegistrationFailed;
        public event Action<UserProfileModel> RegistrationSuccessfullyComplete;

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
        public void TryToRegister()
        {
            RegistrationStarted?.Invoke();
            Register();
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