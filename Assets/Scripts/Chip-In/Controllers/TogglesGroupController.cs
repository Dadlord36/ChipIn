using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;
using Views.ViewElements;

namespace Controllers
{
    [Binding]
    public sealed class TogglesGroupController : MonoBehaviour, INotifyPropertyChanged
    {
        private int _selectedItemIndex;
        private List<Toggle> _radialToggles;

        [HideInInspector] public UnityEvent newItemSelected;

        [Binding]
        public int SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                if (value == _selectedItemIndex) return;
                _selectedItemIndex = value;
                OnPropertyChanged();
                OnNewItemSelected();
            }
        }

        private void Awake()
        {
            CollectToggles();
            SubscribeOnTogglesEvents();
            PrepareItems();
        }

        private void Start()
        {
            _radialToggles[0].IsToggled = true;
        }

        private void SubscribeOnTogglesEvents()
        {
            foreach (var toggle in _radialToggles)
            {
                toggle.toggleClicked.AddListener(OnToggleSwitched);
            }
        }

        private void UnsubscribeFromTogglesEvents()
        {
            foreach (var toggle in _radialToggles)
            {
                toggle.toggleClicked.RemoveListener(OnToggleSwitched);
            }
        }

        private void SwitchOffOtherToggles(Toggle toggle)
        {
            SelectedItemIndex = toggle.Index;

            var otherItems = new List<Toggle>(_radialToggles);
            otherItems.Remove(toggle);
            foreach (var item in otherItems)
            {
                item.IsToggled = false;
            }
        }

        private void OnToggleSwitched(object toggle)
        {
            SwitchOffOtherToggles(toggle as Toggle);
        }

        private void CollectToggles()
        {
            _radialToggles = transform.GetComponentsInChildren<Toggle>().ToList();
        }

        private void PrepareItems()
        {
            for (var index = 0; index < _radialToggles.Count; index++)
            {
                _radialToggles[index].Index = index;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnNewItemSelected()
        {
            newItemSelected?.Invoke();
        }
    }
}