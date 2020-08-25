using Com.TheFallenGames.OSA.Core;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine.Events;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public abstract class SelectableElementsPagesListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData,
        TFillingViewAdapter> : RepositoryBasedListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData, TFillingViewAdapter>
        where TDataType : class
        where TViewConsumableData : class
        where TRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
        where TViewPageViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>, new()
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
    {
        public UnityEvent itemSelected;

        [Binding]
        public uint SelectedIndex { get; set; }

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