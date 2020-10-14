using System;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.DataHelpers;
using Common.Interfaces;
using DataModels.Interfaces;
using Utilities;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    public interface ISelectableListAdapter<TDataType>
    {
        uint SelectedIndex { get; set; }
        TDataType SelectedItemData { get; set; }
        int SelectedItemId { get; set; }

        /// <summary>
        /// Middle item in scroll viewport. Will also call Select() on new middle item sets
        /// </summary>
        BaseItemViewsHolder MiddleItem { get; set; }

        void BindViewHolderSelectionEvent(BaseItemViewsHolder viewHolder, int itemIndex);
        void FindMiddleElement();
    }

    public class SelectableListAdapter<TDataType> : ISelectableListAdapter<TDataType>
    {
        public event Action ItemSelected;

        public SimpleDataHelper<TDataType> Data;
        public List<BaseItemViewsHolder> VisibleItems;
        
        private BaseItemViewsHolder _middleItem;
        public int MiddleElementNumber;

        public uint SelectedIndex { get; set; }
        public TDataType SelectedItemData { get; set; }

        private int _selectedItemId;
        public int SelectedItemId
        {
            get => _selectedItemId;
            set => _selectedItemId = value;
        }

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

        public void BindViewHolderSelectionEvent(BaseItemViewsHolder viewHolder, int itemIndex)
        {
            ((IIdentifiedSelection) viewHolder).ItemSelected += OnItemSelected;
        }

        public void FindMiddleElement()
        {
            MiddleElementNumber = CalculationsUtility.GetMiddle(VisibleItems.Count);
            MiddleItem = VisibleItems[MiddleElementNumber];
        }

        private void OnItemSelected(uint index)
        {
            SelectedIndex = index;
            SelectedItemData = Data[(int) index];
            SelectedItemId = (int) ((IIdentifier) SelectedItemData).Id;
            ItemSelected?.Invoke();
        }
    }
}