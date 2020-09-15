using Com.TheFallenGames.OSA.Core;
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

        [Binding] public uint SelectedIndex { get; set; }

        protected override TViewPageViewHolder CreateViewsHolder(int itemIndex)
        {
            var viewHolder = base.CreateViewsHolder(itemIndex);
            var selection = viewHolder.root.GetComponent<IIdentifiedSelection>();
            selection.IndexInOrder = (uint) itemIndex;
            selection.ItemSelected += OnItemSelected;
            return viewHolder;
        }

        private void OnItemSelected(uint index)
        {
            SelectedIndex = index;
            itemSelected?.Invoke();
        }
    }
}