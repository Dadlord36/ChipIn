using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace Views.BindableProperties
{
    [Binding]
    public sealed class InteractivityPropertyBinding : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private bool isInteractable;

        [Binding]
        public bool IsInteractable
        {
            get => isInteractable;
            set
            {
                if (value == isInteractable) return;
                isInteractable = value;
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