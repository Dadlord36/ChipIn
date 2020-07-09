using System;
using Com.TheFallenGames.OSA.Core;
using Common.Interfaces;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Repositories.Interfaces;
using Repositories.Remote;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public abstract class SelectableElementsPagesListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData,
        TFillingViewAdapter> : RepositoryBasedListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData, TFillingViewAdapter>,
        IIdentifiedSelection
        where TDataType : class
        where TViewConsumableData : class
        where TRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
        where TViewPageViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>, new()
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
    {
        public event Action<uint> ItemSelected;

        protected override TViewPageViewHolder CreateViewsHolder(int itemIndex)
        {
            var viewHolder = base.CreateViewsHolder(itemIndex);
            viewHolder.root.GetComponent<IIdentifiedSelection>().ItemSelected += OnItemSelected;
            return viewHolder;
        }

        private void OnItemSelected(uint index)
        {
            ItemSelected?.Invoke(index);
        }
    }
}