using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements.Icons
{
    [Binding]
    public sealed class IconViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private Sprite _icon;
        private bool _isSelected;

        [Binding]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value == _isSelected) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Sprite Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                IsSelected = value != null;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}