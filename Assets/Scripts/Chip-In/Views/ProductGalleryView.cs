using System;
using System.Collections.Generic;
using Common;
using DataComponents;
using ScriptableObjects.CardsControllers;
using UnityEngine;
using Views.Bars.BarItems;
using Views.InteractiveWindows;
using Views.InteractiveWindows.Interfaces;
using Views.ViewElements;
using Views.ViewElements.Lists.ScrollableList;

namespace Views
{
    public sealed class ProductGalleryView : BaseView, IDropdownList, IRelatedItemsSelection
    {
        public event Action<string> NewCategorySelected;

        [SerializeField] private ItemsDropdownList dropdownList;
        [SerializeField] private ScrollableItemsSelector itemsSelector;
        [SerializeField] private GameObject scrollableMenu;
        [SerializeField] private InfoCardController infoCardController;

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
        public int? CurrentlySelectedOfferId => dropdownList.CurrentlySelectedRelatedId;


        public ProductGalleryView() : base(nameof(ProductGalleryView))
        {
        }

        public void FillDropdownList(Dictionary<int?, string> itemsDictionary)
        {
            dropdownList.FillDropdownList(itemsDictionary);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            itemsSelector.NewItemSelected += OnNewOffersCategorySelected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            itemsSelector.NewItemSelected -= OnNewOffersCategorySelected;
        }

        protected override void OnBeingSwitchedTo()
        {
            base.OnBeingSwitchedTo();
            ShowScrollMenu();
        }

        protected override void OnBeingSwitchedSwitchedFrom()
        {
            base.OnBeingSwitchedSwitchedFrom();
            HideScrollMenu();
        }

        private void HideScrollMenu()
        {
            scrollableMenu.SetActive(false);
        }

        private void ShowScrollMenu()
        {
            scrollableMenu.SetActive(true);
        }

        private void OnNewOffersCategorySelected(Transform obj)
        {
            OnNewCategorySelected(obj.GetComponent<ITitled>().Title);
        }

        private void OnNewCategorySelected(string obj)
        {
            NewCategorySelected?.Invoke(obj);
        }
        
        
        public void HideInfoCard()
        {
            infoCardController.HideCard();
        }

        public void ShowInfoCard()
        {
            infoCardController.ShowCard();
        }
    }
}