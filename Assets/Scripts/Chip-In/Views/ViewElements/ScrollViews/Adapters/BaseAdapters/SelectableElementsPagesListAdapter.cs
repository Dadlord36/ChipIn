﻿using System.Diagnostics;
using Com.TheFallenGames.OSA.Core;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine.Events;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    [Binding]
    public abstract class SelectableElementsPagesListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData,
        TFillingViewAdapter> : RepositoryBasedListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData, TFillingViewAdapter>
        where TDataType : class
        where TViewConsumableData : class
        where TRepository : class, IPaginatedItemsListRepository<TDataType>, ISyncData
        where TViewPageViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>,IIdentifiedSelection,  new()
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
    {
        public UnityEvent itemSelected;

        [Binding] public uint SelectedIndex { get; set; }


        protected override void AdditionItemProcessing(BaseItemViewsHolder viewHolder, int itemIndex)
        {
            var defaultFillingViewPageViewHolder = viewHolder as TViewPageViewHolder;
            Debug.Assert(defaultFillingViewPageViewHolder != null, nameof(defaultFillingViewPageViewHolder) + " != null");
            defaultFillingViewPageViewHolder.ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(uint index)
        {
            SelectedIndex = index;
            itemSelected.Invoke();
        }
    }
}