using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Elements
{
    [Binding]
    public sealed class ItemsLeftViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private uint _itemsLeftNumber;
        public event PropertyChangedEventHandler PropertyChanged;

        [Binding]
        public uint ItemsLeftNumber
        {
            get => _itemsLeftNumber;
            set
            {
                if (value == _itemsLeftNumber) return;
                _itemsLeftNumber = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}