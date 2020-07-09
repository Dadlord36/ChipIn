using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;

namespace Common
{
    [Binding]
    public sealed class PropertyRetranslator : MonoBehaviour, INotifyPropertyChanged
    {
        public UnityEvent propertyRetranslated;  
        private bool _retranslatingValue;

        [Binding]
        public bool RetranslatingValue
        {
            get => _retranslatingValue;
            set
            {
                if (value == _retranslatingValue) return;
                _retranslatingValue = value;
                OnPropertyChanged();
                propertyRetranslated.Invoke();
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