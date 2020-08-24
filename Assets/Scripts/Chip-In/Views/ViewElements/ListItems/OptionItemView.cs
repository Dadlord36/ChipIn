using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Interfaces;
using JetBrains.Annotations;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace Views.ViewElements.ListItems
{
    [Binding]
    public sealed class OptionItemView : UIBehaviour, INotifyPropertyChanged, INotifySelectionWithIdentifier
    {
        public event Action<INotifySelectionWithIdentifier> Selected;
        private bool _isSelected;


        [Binding]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();

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
            }
        }

        public void SetInitialState(bool state)
        {
            _isSelected = state;
        }

        public int Index { get; set; }

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