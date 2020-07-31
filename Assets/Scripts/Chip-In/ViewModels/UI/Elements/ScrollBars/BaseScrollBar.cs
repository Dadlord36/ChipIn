using Common.Interfaces;
using ScriptableObjects.DataSets;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Views.Bars.BarItems;
using Views.ViewElements.Lists.ScrollableList;

namespace ViewModels.UI.Elements.ScrollBars
{
    public abstract class BaseScrollBar<TScrollBarItemView> : MonoBehaviour, IInitialize where TScrollBarItemView : BaseScrollBarItem
    {
        [SerializeField] private Transform scrollBarItemsContainer;
        [SerializeField] protected ItemsScrollBarElements scrollBarElementsData;
        [SerializeField] private TScrollBarItemView prefab;

        private TScrollBarItemView[] _scrollElements;

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

        public void Initialize()
        {
            scrollBarItemsContainer.GetComponent<ScrollItemsUpdater>().Initialize();
            GetComponent<UI_InfiniteScroll>().Init();
        }
    }
}