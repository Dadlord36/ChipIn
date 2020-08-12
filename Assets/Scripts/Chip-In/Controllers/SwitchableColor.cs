using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Controllers
{
    public sealed class SwitchableColor : MonoBehaviour, INotifyPropertyChanged
    {
        private Color _selectedColor;
        private bool _isSwitched;

        public bool IsSwitched
        {
            get => _isSwitched;
            set
            {
                if (value == _isSwitched) return;
                _isSwitched = value;
                OnPropertyChanged();
            }
        }


        [SerializeField] private Color mainColor;
        [SerializeField] private Color secondaryColor;

        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                OnPropertyChanged();
            }
        }

        public void SwitchColor()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}