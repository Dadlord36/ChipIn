using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace Views.ViewElements.ListItems
{
    [Binding]
    public sealed class OptionItemView : UIBehaviour, INotifyPropertyChanged
    {
        public event Action<OptionItemView> Selected;
        private bool _isSelected;

        public int Index { get; set; }
        
        [Binding]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value == _isSelected) return;
                _isSelected = value;
                OnPropertyChanged();

                if (value)
                    OnSelected();
            }
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