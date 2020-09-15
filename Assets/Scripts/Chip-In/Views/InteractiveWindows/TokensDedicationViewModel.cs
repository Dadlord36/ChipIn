using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.UnityEvents;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace Views.InteractiveWindows
{
    [Binding]
    public sealed class TokensDedicationViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private const string Tag = nameof(TokensDedicationViewModel);

        private int _numberAsInt;

        public IntUnityEvent amountConfirmed;


        [Binding]
        public string NumberAsString
        {
            get => _numberAsInt.ToString();
            set
            {
                if (string.IsNullOrEmpty(value) || value == NumberAsString) return;
                _numberAsInt = int.Parse(value);
                OnPropertyChanged();
            }
        }

        private void OnEnable()
        {
            NumberAsString = "0";
        }

        [Binding]
        public void ConfirmButton_OnClick()
        {
            OnAmountConfirmed(_numberAsInt);
        }

        private void OnAmountConfirmed(int value)
        {
            amountConfirmed?.Invoke(value);
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}