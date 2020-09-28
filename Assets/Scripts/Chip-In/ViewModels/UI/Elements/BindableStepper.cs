using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.UnityEvents;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;

namespace ViewModels.UI.Elements
{
    [Binding]
    public sealed class BindableStepper : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private uint minValue;
        [SerializeField] private uint maxValue;

        public UintUnityEvent valueChanged;
        
        private uint _producedNumber;

        [Binding]
        public uint ProducedNumber
        {
            get => _producedNumber;
            set
            {
                if (value == _producedNumber) return;
                _producedNumber = CalculationsUtility.Clamp(value, minValue, maxValue);
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

        private void OnEnable()
        {
            ProducedNumber = minValue;
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