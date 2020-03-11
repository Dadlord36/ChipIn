using System;
using System.Collections.Generic;
using Common;
using DataComponents;
using UnityEngine;
using Views.ViewElements;
using Views.ViewElements.ScrollableList;

namespace Views
{
    public sealed class ProductGalleryView : BaseView, IDropdownList, IRelatedItemsSelection
    {
        public event Action<string> NewCategorySelected;

        [SerializeField] private ItemsDropdownList dropdownList;
        [SerializeField] private ScrollableItemsSelector itemsSelector;
        [SerializeField] private GameObject scrollableMenu;
        public event Action<int> RelatedItemSelected
        {
            add => dropdownList.RelatedItemSelected += value;
            remove => dropdownList.RelatedItemSelected -= value;
        }

        public event Action ItemsListUpdated
        {
            add => dropdownList.ItemsListUpdated += value;
            remove => dropdownList.ItemsListUpdated -= value;
        }
        public string CurrentlySelectedOffersCategory => StringDataComponent.GetStringDataFromComponent(itemsSelector.SelectedItem);
        public int CurrentlySelectedOfferId => dropdownList.CurrentlySelectedOfferId;
        
        public void FillDropdownList(Dictionary<int, string> itemsDictionary)
        {
            dropdownList.FillDropdownList(itemsDictionary);
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            scrollableMenu.SetActive(true);
            itemsSelector.NewItemSelected += OnNewOffersCategorySelected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            scrollableMenu.SetActive(false);
            itemsSelector.NewItemSelected -= OnNewOffersCategorySelected;
        }

        private void OnNewOffersCategorySelected(Transform obj)
        {
            OnNewCategorySelected(StringDataComponent.GetStringDataFromComponent(obj));
        }

        private void OnNewCategorySelected(string obj)
        {
            NewCategorySelected?.Invoke(obj);
        }


    }
}