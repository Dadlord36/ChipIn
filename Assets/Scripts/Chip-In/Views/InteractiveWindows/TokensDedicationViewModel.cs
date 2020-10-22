using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.UnityEvents;
using JetBrains.Annotations;
using Tasking;
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


        public int NumberAsInt
        {
            get => _numberAsInt;
            set
            {
                if (_numberAsInt == value) return;
                _numberAsInt = value;
                NumberAsString = value.ToString();
                OnPropertyChanged();
            }
        }

        [Binding]
        public string NumberAsString
        {
            get => _numberAsInt.ToString();
            private set
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
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}