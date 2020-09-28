﻿using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using UnityEngine.Events;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    public abstract class SelectableListViewAdapter<TOSAPrams, TDataType, TViewPageViewHolder, TViewConsumableData, TFillingViewAdapter>
        : BasedListAdapter<TOSAPrams, TViewPageViewHolder, TDataType, TViewConsumableData, TFillingViewAdapter>
        where TOSAPrams : BaseParamsWithPrefab
        where TDataType : class
        where TViewConsumableData : class
        where TViewPageViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>, new()
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
    {
        public UnityEvent itemSelected;

        [Binding]
        public uint SelectedIndex { get; set; }
        

        protected override void AdditionItemProcessing(BaseItemViewsHolder viewHolder, int itemIndex)
        {
            var selection = viewHolder.root.GetComponentInChildren<IIdentifiedSelection>();
            selection.ItemSelected += SelectNewItem;
        }

        private void SelectNewItem(uint itemIndex)
        {
            if (itemIndex == SelectedIndex) return;
            SelectedIndex = itemIndex;
            OnItemSelected();
        }

        private void OnItemSelected()
        {
            itemSelected?.Invoke();
        }
    }
}