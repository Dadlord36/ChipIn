using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ActionsTranslators;
using Controllers;
using DataModels.RequestsModels;
using JetBrains.Annotations;
using ScriptableObjects.Validations;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class LoginViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private const string Tag = nameof(LoginViewModel);

        [SerializeField] private LoginModelValidation loginModelValidation;
        [SerializeField] private SessionController sessionController;
        [SerializeField] private MainInputActionsTranslator mainInputActionsTranslator;

        private readonly UserLoginRequestModel _userLoginRequestModel = new UserLoginRequestModel();

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            SubscribeToMainInputEventsTranslation();
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            UnsubscribeFromMainInputEventsTranslation();
        }

        private void UnsubscribeFromMainInputEventsTranslation()
        {
            mainInputActionsTranslator.EscapeButtonPressed -= OnEscapeButtonPressed;
        }
        
        private void SubscribeToMainInputEventsTranslation()
        {
            mainInputActionsTranslator.EscapeButtonPressed += OnEscapeButtonPressed;
        }

        private void OnEscapeButtonPressed()
        {
            SwitchToWelcomeView();
        }

        [Binding]
        public string UserEmail
        {
            get => _userLoginRequestModel.Email;
            set
            {
                _userLoginRequestModel.Email = value;
                OnPropertyChanged();
                ValidateLoginData();
            }
        }

        [Binding]
        public string UserPassword
        {
            get => _userLoginRequestModel.Password;
            set
            {
                _userLoginRequestModel.Password = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        [Binding] public bool CanReceiveInput { get; private set; } = true;

        private void ValidateLoginData()
        {
            CanLogin = loginModelValidation.CheckIsValid(_userLoginRequestModel);
            if (CanLogin)
                LogUtility.PrintLog(Tag, "Now can login", this);
        }

        [Binding]
        public void SwitchToRegistrationView()
        {
            SwitchToView(nameof(RegistrationView));
        }

        [Binding]
        public void SwitchToWelcomeView()
        {
            SwitchToView(nameof(WelcomeView));
        }

        [Binding]
        public void LoginButton_Click()
        {
            ProcessLogin();
        }

        private async void ProcessLogin()
        {
            IsPendingLogin = true;
            try
            {
                await sessionController.TryToSignIn(_userLoginRequestModel);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
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