using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.UnityEvents;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace Views.ViewElements
{
    [Binding]
    public sealed class Toggle : UIBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private bool canBeUntoggle;
        public ObjectUnityEvent toggleClicked;

        private bool _isToggled;

        [Binding]
        public bool IsToggled
        {
            get => _isToggled;
            set
            {
                // if (value == _isToggled) return;
                _isToggled = value;
                OnPropertyChanged();
            }
        }

        public void ClickTheToggle()
        {
            ToggleTheToggle();
            OnToggleClicked();
        }

        private void ToggleTheToggle()
        {
            if (IsToggled && !canBeUntoggle)
            {
                return;
            }

            IsToggled = !IsToggled;
        }

        public void SetToggleStateWithoutNotification(bool state)
        {
            _isToggled = state;
        }

        private void OnToggleClicked()
        {
            toggleClicked?.Invoke(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}