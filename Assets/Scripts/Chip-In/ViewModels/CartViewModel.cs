using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views;
using Views.InteractiveWindows;

namespace ViewModels
{
    [Binding]
    public class CartViewModel : BaseItemsListViewModel<CartView>
    {
        [SerializeField] private UserProductsRepository userProductsRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        private bool _infoCanBeShown;

        [Binding]
        public bool InfoCanBeShown
        {
            get => _infoCanBeShown;
            set
            {
                if (value == _infoCanBeShown) return;
                _infoCanBeShown = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public void SwitchInfoCanBeShown()
        {
            if(!ItemIsSelected) return;
            InfoCanBeShown = !InfoCanBeShown;
        }

        [Binding]
        public async Task ShowInfo_OnButtonClick()
        {
            if (!InfoCanBeShown) return;
            try
            {
                await FillInfoCardWithRelatedData(userProductsRepository.CurrentlySelectedIndex);
                RelatedView.ShowInfoCard();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        [Binding]
        public void SwitchItemSelection()
        {
        }

        [Binding]
        public void SwitchToQrCodeView()
        {
            SwitchToView(nameof(QrCodeView));
        }

        protected override async Task FillInfoCardWithRelatedData(int selectedId)
        {
            try
            {
                var selectedItemData = userProductsRepository[selectedId];
                await InfoPanelView.FillWithData(RelatedView, selectedItemData, selectedItemData, selectedItemData, null);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        protected override void OnSelectedItemIndexChanged(int relatedItemIndex)
        {
            base.OnSelectedItemIndexChanged(relatedItemIndex);
            userProductsRepository.CurrentlySelectedIndex = relatedItemIndex;
        }
        

        protected override async void LoadDataAndFillTheList()
        {
            try
            {
                await LoadRepositoryData();
                await FillDropdownList();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private Task LoadRepositoryData()
        {
            return userProductsRepository.LoadDataFromServer();
        }

        protected override Task FillDropdownList()
        {
            var itemsList = userProductsRepository.ItemsData;
            var itemsNamesDictionary = new Dictionary<int?, string>(itemsList.Count);

            for (int i = 0; i < itemsList.Count; i++)
            {
                itemsNamesDictionary.Add(itemsList[i].Id, itemsList[i].Title);
            }

            FillDropdownList(itemsNamesDictionary);
            return Task.CompletedTask;
        }
    }
}