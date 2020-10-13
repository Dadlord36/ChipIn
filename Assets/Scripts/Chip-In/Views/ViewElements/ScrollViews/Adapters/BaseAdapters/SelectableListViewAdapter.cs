using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using UnityEngine.Events;
using UnityWeld.Binding;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    [Binding]
    public abstract class SelectableListViewAdapter<TOSAPrams, TDataType> : BasedListAdapter<TOSAPrams, TDataType>
        where TOSAPrams : BaseParamsWithPrefab
        where TDataType : class
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

        protected int MiddleElementNumber
        {
            get => _selectableListAdapter.MiddleElementNumber;
            set => _selectableListAdapter.MiddleElementNumber = value;
        }

        protected SelectableListViewAdapter()
        {
            _selectableListAdapter = new SelectableListAdapter<TDataType>(Data);
            _selectableListAdapter.ItemSelected += OnItemSelected;
        }

        protected void FindMiddleElement()
        {
            _selectableListAdapter.FindMiddleElement(_VisibleItems, VisibleItemsCount);
        }

        protected override void AdditionItemProcessing(BaseItemViewsHolder viewHolder, int itemIndex)
        {
            _selectableListAdapter.BindViewHolderSelectionEvent(viewHolder, itemIndex);
        }

        private void OnItemSelected()
        {
            itemSelected.Invoke();
        }
    }
}