using System.Diagnostics;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using UnityEngine.Events;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    [Binding]
    public abstract class SelectableListViewAdapter<TOSAPrams, TDataType, TViewPageViewHolder, TSelectionDataType, TViewConsumableData,
        TFillingViewAdapter> : BasedListAdapter<TOSAPrams, TViewPageViewHolder, TDataType, TViewConsumableData, TFillingViewAdapter>
        where TOSAPrams : BaseParamsWithPrefab
        where TDataType : class
        where TViewConsumableData : class
        where TViewPageViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>, IIdentifiedSelection<TSelectionDataType>, new()
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
    {
        public UnityEvent itemSelected;

        private BaseItemViewsHolder _middleItem;
        [Binding] public TSelectionDataType SelectedIndex { get; set; }

        /// <summary>
        /// Middle item in scroll viewport. Will also call Select() on new middle item sets
        /// </summary>
        protected BaseItemViewsHolder MiddleItem
        {
            get => _middleItem;
            set
            {
                if (ReferenceEquals(_middleItem, value)) return;
                _middleItem = value;
                (value as IIdentifiedSelection<TSelectionDataType>)?.Select();
            }
        }

        protected override void AdditionItemProcessing(BaseItemViewsHolder viewHolder, int itemIndex)
        {
            var defaultFillingViewPageViewHolder = viewHolder as TViewPageViewHolder;
            Debug.Assert(defaultFillingViewPageViewHolder != null, nameof(defaultFillingViewPageViewHolder) + " != null");
            defaultFillingViewPageViewHolder.ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(TSelectionDataType itemIndex)
        {
            SelectedIndex = itemIndex;
            itemSelected.Invoke();
        }
        
        protected void FindMiddleElement()
        {
            MiddleElementNumber = CalculationsUtility.GetMiddle(VisibleItemsCount);
            MiddleItem = _VisibleItems[MiddleElementNumber];
        }
    }
}