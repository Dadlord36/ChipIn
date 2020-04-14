using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using ScriptableObjects.Validations;
using UnityEngine;
using UnityWeld.Binding;

namespace Validators
{
    public class BaseTextValidationWithAlert : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private TextValidation validation;
        [SerializeField] private GameObject alertTextField;

        public event Action ValidityChanged;

        private bool _isValid = true;

        [Binding]
        public bool IsValid
        {
            get => _isValid;
            private set
            {
                if (value == _isValid) return;
                _isValid = value;
                OnPropertyChanged();
                OnValidityChanged();
            }
        }

        public void CheckIsValid(object dataToValidate)
        {
            IsValid = validation.CheckIsValid(dataToValidate);
            alertTextField.SetActive(!IsValid);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnValidityChanged()
        {
            ValidityChanged?.Invoke();
        }
    }
}