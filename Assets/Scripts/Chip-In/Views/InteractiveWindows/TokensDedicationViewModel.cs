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
        
        private int _number;

        public IntUnityEvent amountConfirmed;
        
        
        [Binding]
        public int Number
        {
            get => _number;
            set
            {
                if (value == _number) return;
                _number = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public void ConfirmButton_OnClick()
        {
            OnAmountConfirmed(Number);
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