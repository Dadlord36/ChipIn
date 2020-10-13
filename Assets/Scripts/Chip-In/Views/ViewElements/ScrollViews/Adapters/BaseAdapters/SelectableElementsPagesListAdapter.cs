using Com.TheFallenGames.OSA.Core;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine.Events;
using UnityWeld.Binding;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    [Binding]
    public abstract class SelectableElementsPagesListAdapter<TRepository, TDataType> :
        RepositoryBasedListAdapter<TRepository, TDataType>
        where TDataType : class
        where TRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
    
    {
        private readonly SelectableListAdapter<TDataType> _selectableListAdapter;

        public UnityEvent itemSelected;

        [Binding]
        public uint SelectedIndex
        {
            get => _selectableListAdapter.SelectedIndex;
            set => _selectableListAdapter.SelectedIndex = value;
        }

        [Binding]
        public TDataType SelectedItemData
        {
            get => _selectableListAdapter.SelectedItemData;
            set => _selectableListAdapter.SelectedItemData = value;
        }

        private int MiddleElementNumber
        {
            get => _selectableListAdapter.MiddleElementNumber;
            set => _selectableListAdapter.MiddleElementNumber = value;
        }

        protected SelectableElementsPagesListAdapter()
        {
            _selectableListAdapter = new SelectableListAdapter<TDataType>(Data);
            _selectableListAdapter.ItemSelected += OnItemSelected;
        }

        /// <summary>
        /// Middle item in scroll viewport. Will also call Select() on new middle item sets
        /// </summary>
        protected BaseItemViewsHolder MiddleItem
        {
            get => _selectableListAdapter.MiddleItem;
            set => _selectableListAdapter.MiddleItem = value;
        }

        protected override void AdditionItemProcessing(BaseItemViewsHolder viewHolder, int itemIndex)
        {
            _selectableListAdapter.BindViewHolderSelectionEvent(viewHolder, itemIndex);
        }

        protected void FindMiddleElement()
        {
            _selectableListAdapter.FindMiddleElement(_VisibleItems, VisibleItemsCount);
        }

        private void OnItemSelected()
        {
            itemSelected.Invoke();
        }
    }
}