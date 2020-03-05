﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HttpRequests.RequestsProcessors.PutRequests;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Elements
{
    [Binding]
    public sealed class PasswordChangingViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private UserProfileRemoteRepository userProfileRemoteRepository;
        [SerializeField] private PasswordAnalyzer passwordAnalyzer;

        private bool _canChangePassword;
        private string _currentPassword;

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
        public string CurrentPassword
        {
            get => _currentPassword;
            set
            {
                if (value == _currentPassword) return;
                _currentPassword = value;
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
        public async void Confirm_OnClick()
        {
            if (await TryChangePassword())
                HideView();
        }

        [Binding]
        public void Close_OnClick()
        {
            HideView();
        }

        private void HideView()
        {
            View.Hide();
        }

        private async Task<bool> TryChangePassword()
        {
            try
            {
                return await UserProfileDataStaticRequestsProcessor.TryChangeUserProfilePassword(authorisationDataRepository,
                    new UserProfilePasswordChangingModel
                    {
                        Password = this.Password, PasswordConfirmation = PasswordRepeat,
                        CurrentPassword = this.CurrentPassword
                    });
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }

            return false;
        }


        private void CheckIfCanConfirmChange()
        {
            CanChangePassword = passwordAnalyzer.CheckIfPasswordsAreMatchAndItIsValid() && passwordAnalyzer.IsPasswordValid(CurrentPassword);
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}