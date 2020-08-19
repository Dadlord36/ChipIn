using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;
using Views.ViewElements.ListItems;

namespace Controllers
{
    [Binding]
    public sealed class TogglesGroupController : MonoBehaviour, INotifyPropertyChanged
    {
        private int _selectedItemIndex;
        private List<OptionItemView> _optionItemViews;

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
            _optionItemViews[0].IsSelected = true;
        }

        private void SubscribeOnTogglesEvents()
        {
            foreach (var toggle in _optionItemViews)
            {
                toggle.Selected += OnToggleSwitched;
            }
        }

        private void UnsubscribeFromTogglesEvents()
        {
            foreach (var optionItemView in _optionItemViews)
            {
                optionItemView.Selected -= OnToggleSwitched;
            }
        }

        private void OnToggleSwitched(OptionItemView optionItemView)
        {
            SwitchOffOtherToggles(optionItemView);
        }

        private void SwitchOffOtherToggles(OptionItemView optionItemView)
        {
            SelectedItemIndex = optionItemView.Index;

            var otherItems = new List<OptionItemView>(_optionItemViews);
            otherItems.Remove(optionItemView);
            foreach (var item in otherItems)
            {
                item.IsSelected = false;
            }
        }
        
        private void CollectToggles()
        {
            _optionItemViews = transform.GetComponentsInChildren<OptionItemView>().ToList();
        }

        private void PrepareItems()
        {
            for (var index = 0; index < _optionItemViews.Count; index++)
            {
                _optionItemViews[index].Index = index;
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