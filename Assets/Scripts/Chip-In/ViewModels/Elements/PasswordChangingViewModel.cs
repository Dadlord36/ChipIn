using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Controllers;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;

namespace ViewModels.Elements
{
    public interface IPasswordChangingViewModel
    {
        event Action<string> NewPasswordApproved;
        RectTransform RootTransform { get; }
    }

    [Binding]
    public sealed class PasswordChangingViewModel : MonoBehaviour, INotifyPropertyChanged, IPasswordChangingViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<string> NewPasswordApproved;

        public RectTransform RootTransform => transform as RectTransform;

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
        public void Confirm_OnClick()
        {
            OnNewPasswordApproved(Password);
            ClearFields();
            MakeFormInteractive();
        }

        [Binding]
        public void Close_OnClick()
        {
            HideView();
            MakeFormInteractive();
        }

        private void HideView()
        {
            gameObject.SetActive(false);
            ClearFields();
        }

        private void MakeFormInteractive()
        {
            FindObjectOfType<FormInteractivityController>().Interactive = true;
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
            NewPasswordApproved?.Invoke(approvedPassword);
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}