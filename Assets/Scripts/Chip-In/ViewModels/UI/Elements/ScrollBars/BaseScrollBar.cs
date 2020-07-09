using System;
using Common.Interfaces;
using ScriptableObjects.DataSets;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Views.Bars.BarItems;
using Views.ViewElements.Lists.ScrollableList;

namespace ViewModels.UI.Elements.ScrollBars
{
    public abstract class BaseScrollBar<TScrollBarItemView> : MonoBehaviour,IInitialize where TScrollBarItemView : BaseScrollBarItem
    {
        [SerializeField] private Transform scrollBarItemsContainer;
        [SerializeField] private ItemsScrollBarElements scrollBarElementsData;
        [SerializeField] private TScrollBarItemView prefab;

        public event Action<ITitled> NewItemSelected;


        private TScrollBarItemView[] _scrollElements;
        private ScrollableItemsSelector ScrollableItemsSelector => scrollBarItemsContainer.GetComponent<ScrollableItemsSelector>();


        private void OnEnable()
        {
            ScrollableItemsSelector.NewItemSelected += OnCenterItemChanged;
        }

        private void OnDisable()
        {
            ScrollableItemsSelector.NewItemSelected -= OnCenterItemChanged;
        }

        private void OnCenterItemChanged(Transform itemTransform)
        {
            OnNewItemSelected(itemTransform.GetComponent<ITitled>());
        }

        public void RemoveScrollBarItems()
        {
            for (int i = 0; i < _scrollElements.Length; i++)
            {
                Destroy(_scrollElements[i].gameObject);
            }
        }

        public void FillContainerWithItems()
        {
            var itemsData = scrollBarElementsData.ItemsData;
            _scrollElements = new TScrollBarItemView[itemsData.Length];
            for (int i = 0; i < itemsData.Length; i++)
            {
                _scrollElements[i] = Instantiate(prefab, scrollBarItemsContainer);
                _scrollElements[i].Set(itemsData[i]);
            }

            Initialize();
        }


        private void OnNewItemSelected(ITitled obj)
        {
            NewItemSelected?.Invoke(obj);
        }

        public void Initialize()
        {
            scrollBarItemsContainer.GetComponent<ScrollItemsUpdater>().Initialize();
            GetComponent<UI_InfiniteScroll>().Init();
        }
        
       
    }
}