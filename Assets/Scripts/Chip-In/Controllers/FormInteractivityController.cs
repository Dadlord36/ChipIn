using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace Controllers
{
    [Binding]
    public sealed class FormInteractivityController : MonoBehaviour, INotifyPropertyChanged
    {
        private bool _interactive = true;

        [Binding]
        public bool Interactive
        {
            get => _interactive;
            set
            {
                if (value == _interactive) return;
                _interactive = value;
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