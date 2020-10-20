using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using UnityEngine.Events;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.ViewHolders;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    [Binding]
    public abstract class SelectableListViewAdapter<TOSAPrams, TDataType> : BasedListAdapter<TOSAPrams, TDataType>
        where TOSAPrams : BaseParamsWithPrefab
        where TDataType : class
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

        [Binding]
        public int SelectedItemId
        {
            get => _selectableListAdapterMediator.SelectedItemId;
            set => _selectableListAdapterMediator.SelectedItemId = value;
        }

        [Binding]
        public BaseItemViewsHolder MiddleItem
        {
            get => _selectableListAdapterMediator.MiddleItem;
            set => _selectableListAdapterMediator.MiddleItem = value;
        }

        [Binding]
        protected int MiddleElementNumber
        {
            get => _selectableListAdapterMediator.MiddleElementNumber;
            set => _selectableListAdapterMediator.MiddleElementNumber = value;
        }

        protected SelectableListViewAdapter()
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

        public void BindViewHolderSelectionEvent(BaseItemViewsHolder viewHolder, int itemIndex)
        {
            _selectableListAdapterMediator.BindViewHolderSelectionEvent(viewHolder, itemIndex);
        }

        protected void FindMiddleElement()
        {
            _selectableListAdapterMediator.FindMiddleElement();
        }

        protected override void AdditionItemProcessing(DefaultFillingViewPageViewHolder<TDataType> viewHolder, int itemIndex)
        {
            _selectableListAdapterMediator.BindViewHolderSelectionEvent(viewHolder, itemIndex);
        }

        private void OnItemSelected()
        {
            itemSelected.Invoke();
        }
    }
}