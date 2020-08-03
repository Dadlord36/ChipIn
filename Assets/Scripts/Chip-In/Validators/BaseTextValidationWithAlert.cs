using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using ScriptableObjects.Validations;
using UnityEngine;
using UnityWeld.Binding;

namespace Validators
{
    public interface IValidationWithAlert
    {
        void ShowAlertIfIsNotValid();
        bool IsValid { get; }
    }

    [Binding]
    public abstract class BaseTextValidationWithAlert<T> : MonoBehaviour, INotifyPropertyChanged, IValidationWithAlert
    {
        [SerializeField] private TextValidation validation;

        [Binding] public T TextToValidate { get; set; }

        private object PropertyToValidate => TextToValidate;
        private bool _isValid = true;
        private bool _showAlert;

        [Binding]
        public bool IsValid
        {
            get => _isValid;
            private set
            {
                if (value == _isValid) return;
                _isValid = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool ShowAlert
        {
            get => _showAlert;
            set
            {
                if (value == _showAlert) return;
                _showAlert = value;
                OnPropertyChanged();
            }
        }


        [Binding]
        public void HideAlertText()
        {
            ShowAlert = false;
        }
        
        private bool CheckIsValid()
        {
            return IsValid = validation.CheckIsValid(PropertyToValidate);
        }

        public void ShowAlertIfIsNotValid()
        {
            ShowAlert = !CheckIsValid();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}