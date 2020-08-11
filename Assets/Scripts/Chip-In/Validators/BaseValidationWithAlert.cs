using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace Validators
{
    [Binding]
    public abstract class BaseValidationWithAlert : MonoBehaviour, INotifyPropertyChanged, IValidationWithAlert
    {
        private bool _isValid = true;
        private bool _showAlert;

        [Binding]
        public bool IsValid
        {
            get => _isValid;
            protected set
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

        protected abstract bool CheckIsValid();

        public void ShowAlertIfIsNotValid()
        {
            ShowAlert = !CheckIsValid();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}