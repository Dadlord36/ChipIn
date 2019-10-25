using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels;
using HttpRequests;
using JetBrains.Annotations;
using ScriptableObjects.Validations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public sealed class LoginViewModel : BaseViewModel, INotifyPropertyChanged
    {
        [SerializeField] private LoginModelValidation loginModelValidation;
        private readonly UserLoginModel _userLoginModel = new UserLoginModel();
        [Binding]
        public string UserEmail
        {
            get => _userLoginModel.Email;
            set
            {
                _userLoginModel.Email = value;
                ValidateLoginData();
            }
        }
        [Binding]
        public string UserPassword
        {
            get => _userLoginModel.Password;
            set
            {
                _userLoginModel.Password = value;
                ValidateLoginData();
            }
        }
        private bool _canLogin;

        [Binding]
        public bool CanLogin
        {
            get => _canLogin;
            set
            {
                _canLogin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanLogin)));
            }
        }

        private bool _pendingLogin;
        [Binding]
        public bool IsPendingLogin
        {
            get => _pendingLogin;
            set
            {
                _pendingLogin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPendingLogin)));
            }
        }


        [Binding] public bool CanReceiveInput { get; private set; } = true;

        private void ValidateLoginData()
        {
            CanLogin = loginModelValidation.CheckIsValid(_userLoginModel);
            if (CanLogin)
                Debug.Log("Now can login");
        }

        [Binding]
        public void LoginToAccount()
        {
            IsPendingLogin = true;
            ProcessLogin();
        }

        private async void ProcessLogin()
        {
            var responseData = await new LoginRequestProcessor().SendRequest(_userLoginModel);

            if (responseData.ResponseMessage.IsSuccessStatusCode)
            {
                Debug.Log($"Response is successful");
                Debug.Log(responseData.ResponseData.user.ToString());
            }

            IsPendingLogin = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}