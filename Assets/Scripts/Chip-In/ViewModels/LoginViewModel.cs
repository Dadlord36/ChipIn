﻿using System;
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
        [SerializeField] private LoginModelValidation loginModelValidation;
        [SerializeField] private SessionController sessionController;
        [SerializeField] private MainInputActionsTranslator mainInputActionsTranslator;

        private readonly UserLoginRequestModel _userLoginRequestModel = new UserLoginRequestModel();

        public LoginViewModel() : base(nameof(LoginViewModel))
        {
        }

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

        [Binding] public bool IsMerchant { get; set; }

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
        public async void LoginButton_Click()
        {
            try
            {
                IsPendingLogin = true;
                await ProcessLoginAsync().ConfigureAwait(true);
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
            finally
            {
                IsPendingLogin = false;
            }
        }

        [Binding]
        public async void ProceedAsGuestsButton_OnClicked()
        {
            try
            {
                IsPendingLogin = true;
                await ProcessLoginAsGuestAsync().ConfigureAwait(true);
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
            finally
            {
                IsPendingLogin = false;
            }
        }

        private Task ProcessLoginAsync()
        {
            return sessionController.TryToSignIn(_userLoginRequestModel);
        }

        private Task ProcessLoginAsGuestAsync()
        {
            return sessionController.TryRegisterAndLoginAsGuest();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}