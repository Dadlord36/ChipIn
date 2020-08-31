using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Interfaces;
using Common.UnityEvents;
using JetBrains.Annotations;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace Views.ViewElements.ListItems
{
    [Binding]
    public sealed class OptionItemView : UIBehaviour, INotifyPropertyChanged, INotifySelectionWithIdentifier
    {
        public BoolUnityEvent stateChanged;
        
        private bool _isSelected;


        public event Action<INotifySelectionWithIdentifier> Selected;

        [Binding]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
                stateChanged.Invoke(value);

                if (value)
                    OnSelected();
            }
        }
        
        [Binding]
        public bool InitialState
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
                stateChanged.Invoke(value);
            }
        }
        
        public int Index { get; set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetInitialState(IsSelected);
        }

        public void SetInitialState(bool state)
        {
            InitialState = state;
        }

        private void OnSelected()
        {
            Selected?.Invoke(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}