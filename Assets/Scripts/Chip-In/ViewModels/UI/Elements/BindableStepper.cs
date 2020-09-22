using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.UnityEvents;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements
{
    [Binding]
    public sealed class BindableStepper : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private int minValue;
        [SerializeField] private int maxValue;

        public IntUnityEvent valueChanged;
        
        private int _producedNumber;

        [Binding]
        public int ProducedNumber
        {
            get => _producedNumber;
            set
            {
                if (value == _producedNumber) return;
                _producedNumber = Mathf.Clamp(value, minValue, maxValue);
                OnPropertyChanged();
                OnValueChanged();
            }
        }

        [Binding]
        public void Adjust()
        {
            ProducedNumber++;
        }

        [Binding]
        public void Subtract()
        {
            ProducedNumber--;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnValueChanged()
        {
            valueChanged?.Invoke(_producedNumber);
        }
    }
}