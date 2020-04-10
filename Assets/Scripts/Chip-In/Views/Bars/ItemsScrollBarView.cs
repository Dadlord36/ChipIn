using System;
using ScriptableObjects.DataSets;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Utilities;
using Views.Bars.BarItems;
using Views.ViewElements.Lists.ScrollableList;

namespace Views.Bars
{
    public class ItemsScrollBarView : BaseView
    {
        [SerializeField] private ItemsScrollBarElements scrollBarElementsData;
        [SerializeField] private Transform itemsContainer;
        [SerializeField] private UI_InfiniteScroll infiniteScroll;
        [SerializeField] private ScrollItemsUpdater itemsUpdater;

        public event Action<string> NewItemSelected;

        private bool _isInitialized;
        private ScrollBarItemWithTitleAndIconView[] _scrollBarItemViews;

        private void Initialized()
        {
            if(_isInitialized) return;
            
            // InstantiateItems();

            infiniteScroll.Init();
            itemsUpdater.Initialize();
            _isInitialized = true;
        }

        public void Activate()
        {
            Initialized();
            SubscribeToBarItems();
        }

        public void Deactivate()
        {
            UnsubscribeFromBarItems();
        }

        private void SubscribeToBarItems()
        {
            for (int i = 0; i < _scrollBarItemViews.Length; i++)
            {
                _scrollBarItemViews[i].Selected += OnNewItemSelected;
            }
        }

        private void UnsubscribeFromBarItems()
        {
            for (int i = 0; i < _scrollBarItemViews.Length; i++)
            {
                _scrollBarItemViews[i].Selected -= OnNewItemSelected;
            }
        }

        private void OnNewItemSelected(string itemTitle)
        {
            LogUtility.PrintLog(nameof(ScrollBarItemWithTitleAndIconView),$"{itemTitle} item selected");
            NewItemSelected?.Invoke(itemTitle);
        }

 
    }
}