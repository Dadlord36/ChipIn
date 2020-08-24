using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements.Text
{
    [Binding]
    public sealed class ColorSwitch : UIBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private Color mainColor;
        [SerializeField] private Color alternativeColor;
        
        
        private Color _selectedColor;
        private bool _isSwitched;
        
        public bool IsSwitched
        {
            get => _isSwitched;
            set
            {
                _isSwitched = value;
                SelectedColor = value ? alternativeColor : mainColor;
            }
        }

        [Binding]
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (value.Equals(_selectedColor)) return;
                _selectedColor = value;
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