using System.Diagnostics;
using Com.TheFallenGames.OSA.Core;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine.Events;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    [Binding]
    public abstract class SelectableElementsPagesListAdapter<TRepository, TDataType, TViewPageViewHolder, TSelectionDataType, TViewConsumableData,
        TFillingViewAdapter> : RepositoryBasedListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData, TFillingViewAdapter>
        where TDataType : class
        where TViewConsumableData : class
        where TRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
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

        protected void FindMiddleElement()
        {
            MiddleElementNumber = CalculationsUtility.GetMiddle(VisibleItemsCount);
            MiddleItem = _VisibleItems[MiddleElementNumber];
        }

        private void OnItemSelected(TSelectionDataType index)
        {
            SelectedIndex = index;
            itemSelected.Invoke();
        }
    }
}