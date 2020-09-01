using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.TheFallenGames.OSA.Core;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters.Parameters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    public interface IResettableAsync
    {
        Task ResetAsync();
    }

    [Binding]
    public abstract class RepositoryBasedListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData,
        TFillingViewAdapter> : BasedListAdapter<RepositoryPagesAdapterParameters, TViewPageViewHolder, TDataType, TViewConsumableData, TFillingViewAdapter>,
        IResettableAsync
        where TDataType : class
        where TViewConsumableData : class
        where TRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
        where TViewPageViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>, new()
    {
        [SerializeField] private TRepository pagesPaginatedRepository;


        public UnityEvent startedFetching;
        public UnityEvent endedFetching;

        private uint TotalCapacity => pagesPaginatedRepository.TotalItemsNumber;


        private bool _fetching;
        private bool _loadedAll;
        private uint _retrievingItemsStartingIndex;


        private void ResetStateVariables()
        {
            _fetching = _loadedAll = false;
            _retrievingItemsStartingIndex = 0;
        }

        public async Task ResetAsync()
        {
            ResetStateVariables();

            if (Data.Count > 0)
            {
                Data.RemoveItemsFromStart(Data.Count);
            }

            pagesPaginatedRepository.Clear();
            OnStartedFetching();
            try
            {
                await pagesPaginatedRepository.LoadDataFromServer().ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            finally
            {
                OnEndedFetching();
            }
        }

        private void SetInteractivity(bool state)
        {
            Parameters.SetScrollInteractivity(state);
        }


        protected override async void Update()
        {
            base.Update();

            if (!IsInitialized)
                return;

            if (_fetching)
                return;

            if (_loadedAll)
                return;

            if (Data == null)
                return;

            int lastVisibleItemItemIndex = -1;
            if (_VisibleItemsCount > 0)
            {
                SetInteractivity(CheckIfShouldAllowScrolling());

                bool CheckIfShouldAllowScrolling()
                {
                    return TotalCapacity > _VisibleItems.Count;
                }

                lastVisibleItemItemIndex = _VisibleItems.Last().ItemIndex;
            }

            var numberOfItemsBelowLastVisible = Data.Count - (lastVisibleItemItemIndex + 1);

            // If the number of items available below the last visible (i.e. the bottom-most one, in our case) is less than <adapterParams.preFetchedItemsCount>,
            // get more
            if (numberOfItemsBelowLastVisible >= _Params.PreFetchedItemsCount) return;
            uint newPotentialNumberOfItems = (uint) (Data.Count + _Params.PreFetchedItemsCount);

            newPotentialNumberOfItems = Math.Min(newPotentialNumberOfItems, TotalCapacity);

            if (newPotentialNumberOfItems <= Data.Count) return;
            try
            {
                _fetching = true;
                await StartPreFetchingAsync((uint) (newPotentialNumberOfItems - Data.Count)).ConfigureAwait(true);
            }
            catch (ArgumentOutOfRangeException e)
            {
                LogUtility.PrintLog(Tag, e.Message);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            finally
            {
                _fetching = false;
            }
        }

        private Task StartPreFetchingAsync(uint additionalItems)
        {
            OnStartedFetching();
            return FetchItemModelsFromServerAsync(additionalItems);
        }

        private async Task FetchItemModelsFromServerAsync(uint maxCount)
        {
            AsyncOperationCancellationController.CancelOngoingTask();
            var items = await pagesPaginatedRepository.CreateGetItemsRangeTask(_retrievingItemsStartingIndex, maxCount)
                .ConfigureAwait(true);
            _retrievingItemsStartingIndex += (uint) items.Count;
            _loadedAll = Data.Count == TotalCapacity;

            if (items.Count > 0)
                Data.InsertItemsAtEnd(items as IList<TDataType>, _Params.FreezeContentEndEdgeOnCountChange);

            ItemsListIsEmpty = Data.Count == 0;
            OnEndedFetching();
        }

        private void OnStartedFetching()
        {
            startedFetching?.Invoke();
        }

        private void OnEndedFetching()
        {
            endedFetching?.Invoke();
        }
    }
}