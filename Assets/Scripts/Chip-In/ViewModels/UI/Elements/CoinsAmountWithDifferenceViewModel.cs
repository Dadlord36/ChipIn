using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements
{
    [Binding]
    public sealed class CoinsAmountWithDifferenceViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private int _coinsAmount;
        private string _difference;
        private int _differenceAsInt;

        [Binding]
        public int CoinsAmount
        {
            get => _coinsAmount;
            set
            {
                if (value == _coinsAmount) return;
                _coinsAmount = value;
                OnPropertyChanged();
            }
        }

        public int DifferenceAsInt
        {
            get => _differenceAsInt;
            set
            {
                if (value == _differenceAsInt) return;
                _differenceAsInt = value;
                OnPropertyChanged();
                Difference = $"({value.ToString()})";
            }
        }

        [Binding]
        public string Difference
        {
            get => _difference;
            set
            {
                if (value == _difference) return;
                _difference = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}