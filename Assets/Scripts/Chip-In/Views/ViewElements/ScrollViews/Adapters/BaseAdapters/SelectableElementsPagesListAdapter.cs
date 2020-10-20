using Com.TheFallenGames.OSA.Core;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine.Events;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    [Binding]
    public abstract class SelectableItemsRepositoryListAdapter<TRepository, TDataType> : RepositoryBasedListAdapter<TRepository, TDataType>,
        ISelectableListAdapter<TDataType> where TDataType : class
        where TRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
    {
        private readonly SelectableListAdapterMediator<TDataType> _selectableListAdapterMediator;

        public UnityEvent itemSelected;


        [Binding]
        public uint SelectedIndex
        {
            get => _selectableListAdapterMediator.SelectedIndex;
            set => _selectableListAdapterMediator.SelectedIndex = value;
        }

        [Binding]
        public TDataType SelectedItemData
        {
            get => _selectableListAdapterMediator.SelectedItemData;
            set => _selectableListAdapterMediator.SelectedItemData = value;
        }

        private int MiddleElementNumber
        {
            get => _selectableListAdapterMediator.MiddleElementNumber;
            set => _selectableListAdapterMediator.MiddleElementNumber = value;
        }

        [Binding]
        public int SelectedItemId
        {
            get => _selectableListAdapterMediator.SelectedItemId;
            set => _selectableListAdapterMediator.SelectedItemId = value;
        }

        /// Middle item in scroll viewport. Will also call Select() on new middle item sets
        /// </summary>
        public BaseItemViewsHolder MiddleItem
        {
            get => _selectableListAdapterMediator.MiddleItem;
            set => _selectableListAdapterMediator.MiddleItem = value;
        }

        protected override void AdditionItemProcessing(DefaultFillingViewPageViewHolder<TDataType> viewHolder, int itemIndex)
        {
            BindViewHolderSelectionEvent(viewHolder, itemIndex);
        }

        public void BindViewHolderSelectionEvent(BaseItemViewsHolder viewHolder, int itemIndex)
        {
            _selectableListAdapterMediator.BindViewHolderSelectionEvent(viewHolder, itemIndex);
        }

        public void FindMiddleElement()
        {
            _selectableListAdapterMediator.FindMiddleElement();
        }

        protected SelectableItemsRepositoryListAdapter()
        {
            _selectableListAdapterMediator = new SelectableListAdapterMediator<TDataType>();
            _selectableListAdapterMediator.ItemSelected += OnItemSelected;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _selectableListAdapterMediator.Data = Data;
            _selectableListAdapterMediator.VisibleItems = _VisibleItems;
        }

        private void OnItemSelected()
        {
            itemSelected.Invoke();
        }
    }
}