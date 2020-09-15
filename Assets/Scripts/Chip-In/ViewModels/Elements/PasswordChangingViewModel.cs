﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Behaviours;
using Common.UnityEvents;
using Controllers;
using HttpRequests.RequestsProcessors.PutRequests;
using JetBrains.Annotations;
using Repositories.Remote;
using RequestsStaticProcessors;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.Elements
{
    public interface IPasswordChangingViewModel
    {
        event Action<string> NewPasswordApproved;
        RectTransform RootTransform { get; }
    }

    [Binding]
    public sealed class PasswordChangingViewModel : AsyncOperationsMonoBehaviour, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public StringUnityEvent newPasswordApproved;
        public RectTransform RootTransform => transform as RectTransform;
        [SerializeField] private PasswordAnalyzer passwordAnalyzer;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private AlertCardController alertCardController;


        private bool _canChangePassword;
        private string _currentPassword;

        [Binding]
        public string Password
        {
            get => passwordAnalyzer.OriginalPassword;
            set
            {
                passwordAnalyzer.OriginalPassword = value;
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
            if (!ValidationHelper.CheckIfAllFieldsAreValid(this))
            {
                return;
            }

            try
            {
                IsAwaitingProcess = true;
                var response = await ProfileDataStaticRequestsProcessor.TryChangeProfilePassword(out TasksCancellationTokenSource,
                    authorisationDataRepository, new UserProfilePasswordChangingModel
                    {
                        CurrentPassword = CurrentPassword, Password = Password,
                        PasswordConfirmation = PasswordRepeat
                    });

                if (response.Success)
                {
                    alertCardController.ShowAlertWithText("Password changed successfully");

                    HideView();
                    OnNewPasswordApproved(Password);
                }
                else
                {
                    alertCardController.ShowAlertWithText("Failed to change password");
                }

                ClearFields();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsAwaitingProcess = false;
            }
        }

        [Binding]
        public void Close_OnClick()
        {
            HideView();
        }

        private void HideView()
        {
            gameObject.SetActive(false);
            ClearFields();
        }
        
        private void ClearFields()
        {
            CurrentPassword = PasswordRepeat = Password = string.Empty;
        }

        private void CheckIfCanConfirmChange()
        {
            CanChangePassword = passwordAnalyzer.CheckIfPasswordsAreMatchAndItIsValid() &&
                                passwordAnalyzer.IsPasswordValid(CurrentPassword);
        }

        private void OnNewPasswordApproved(in string approvedPassword)
        {
            newPasswordApproved?.Invoke(approvedPassword);
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}