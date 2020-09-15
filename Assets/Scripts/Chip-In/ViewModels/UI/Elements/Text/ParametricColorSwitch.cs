using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using ScriptableObjects.Parameters;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.UI.Elements.Text
{
    [Binding]
    public sealed class ParametricColorSwitch : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private ColorsPairParameter colorsPairParameter;

        private Color _selectedColor;
        private bool _isSwitched;

        public bool IsSwitched
        {
            get => _isSwitched;
            set
            {
                _isSwitched = value;
                SelectedColor = value ? colorsPairParameter.value2 : colorsPairParameter.value1;
            }
        }

        public bool InitialState
        {
            get => _isSwitched;
            set
            {
                _isSwitched = value;
                InitialColor = value ? colorsPairParameter.value2 : colorsPairParameter.value1;
            }
        }
        
        [Binding]
        public Color InitialColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
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