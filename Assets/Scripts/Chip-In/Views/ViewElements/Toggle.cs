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
    public sealed class Toggle : UIBehaviour, INotifyPropertyChanged, IPointerClickHandler
    {
        [SerializeField] private bool canBeUntoggle;
        public ObjectUnityEvent toggleClicked;

        private bool _isToggled;

        public int Index { get; set; }

        [Binding]
        public bool IsToggled
        {
            get => _isToggled;
            set
            {
                if (value == _isToggled) return;
                _isToggled = value;
                OnPropertyChanged();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsToggled && !canBeUntoggle)
            {
                return;
            }

            IsToggled = !IsToggled;
            OnToggleClicked();
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