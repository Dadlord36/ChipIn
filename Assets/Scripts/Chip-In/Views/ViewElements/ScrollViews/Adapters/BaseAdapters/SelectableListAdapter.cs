using System;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.DataHelpers;
using Common.Interfaces;
using Utilities;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    public class SelectableListAdapter<TDataType>
    {
        public event Action ItemSelected;

        private BaseItemViewsHolder _middleItem;
        private readonly SimpleDataHelper<TDataType> _data;
        public int MiddleElementNumber;

        public uint SelectedIndex { get; set; }
        public TDataType SelectedItemData { get; set; }

        /// <summary>
        /// Middle item in scroll viewport. Will also call Select() on new middle item sets
        /// </summary>
        public BaseItemViewsHolder MiddleItem
        {
            get => _middleItem;
            set
            {
                if (ReferenceEquals(_middleItem, value)) return;
                _middleItem = value;
                (value as IIdentifiedSelection)?.Select();
            }
        }

        public SelectableListAdapter(SimpleDataHelper<TDataType> data)
        {
            _data = data;
        }

        public void BindViewHolderSelectionEvent(BaseItemViewsHolder viewHolder, int itemIndex)
        {
            ((IIdentifiedSelection) viewHolder).ItemSelected += OnItemSelected;
        }

        public void FindMiddleElement(List<BaseItemViewsHolder> _VisibleItems, int visibleItemsCount)
        {
            MiddleElementNumber = CalculationsUtility.GetMiddle(visibleItemsCount);
            MiddleItem = _VisibleItems[MiddleElementNumber];
        }

        private void OnItemSelected(uint index)
        {
            SelectedIndex = index;
            SelectedItemData = _data[(int) index];
            ItemSelected.Invoke();
        }
    }
}