using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace Views.ViewElements
{
    [Binding]
    public sealed class SignedNumberView : MonoBehaviour, INotifyPropertyChanged
    {
        private int _signedNumberValue;

        [Binding]
        public int SignedNumberValue
        {
            get => _signedNumberValue;
            set
            {
                _signedNumberValue = value;
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