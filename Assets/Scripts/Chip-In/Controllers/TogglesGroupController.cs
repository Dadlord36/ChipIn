using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;

namespace Controllers
{
    [Binding]
    public sealed class TogglesGroupController : MonoBehaviour, INotifyPropertyChanged
    {
        private int _selectedItemIndex;
        private List<INotifySelectionWithIdentifier> _selectableOptions;

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
            _selectableOptions[0].SetInitialState(true);
        }

        private void SubscribeOnTogglesEvents()
        {
            foreach (var toggle in _selectableOptions)
            {
                toggle.Selected += SwitchOffOtherToggles;
            }
        }

        private void UnsubscribeFromTogglesEvents()
        {
            foreach (var optionItemView in _selectableOptions)
            {
                optionItemView.Selected -= SwitchOffOtherToggles;
            }
        }


        private void SwitchOffOtherToggles(INotifySelectionWithIdentifier option)
        {
            SelectedItemIndex = option.Index;
            var otherItems = GetItemsExcept(option, _selectableOptions);

            foreach (var item in otherItems)
            {
                item.IsSelected = false;
            }
        }

        private static List<T> GetItemsExcept<T>(int itemByIndex, IEnumerable<T> inList)
        {
            var list = new List<T>(inList);
            list.RemoveAt(itemByIndex);
            return list;
        }
        
        private static List<T> GetItemsExcept<T>(T item, IEnumerable<T> inList)
        {
            var list = new List<T>(inList);
            list.Remove(item);
            return list;
        }

        private void CollectToggles()
        {
            _selectableOptions = transform.GetComponentsInChildren<INotifySelectionWithIdentifier>().ToList();
        }

        private void PrepareItems()
        {
            for (var index = 0; index < _selectableOptions.Count; index++)
            {
                _selectableOptions[index].Index = index;
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