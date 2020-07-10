using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.DataHelpers;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters.Parameters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class RepositoryBasedListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData, TFillingViewAdapter> :
        OSA<RepositoryPagesAdapterParameters, TViewPageViewHolder>
        where TDataType : class
        where TViewConsumableData : class
        where TRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
        where TViewPageViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>, new()
    {
        private readonly string Tag;

        [SerializeField] private TRepository pagesPaginatedRepository;

        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();
        private readonly TFillingViewAdapter _fillingViewAdapter = new TFillingViewAdapter();

        public event Action StartedFetching;
        public event Action EndedFetching;

        public RepositoryBasedListAdapter()
        {
            Tag = GetType().Name;
        }


        // Helper that stores data and notifies the adapter when items count changes
        // Can be iterated and can also have its elements accessed by the [] operator
        private SimpleDataHelper<TDataType> Data { get; set; }
        private uint TotalCapacity => pagesPaginatedRepository.TotalItemsNumber;

        #region OSA implementation

        private bool _fetching;
        private bool _loadedAll;
        private uint _retrievingItemsStartingIndex;

        private void ResetStateVariables()
        {
            _fetching = _loadedAll = false;
            _retrievingItemsStartingIndex = 0;
        }

        public Task Initialize()
        {
            ResetStateVariables();
            if (Data != null)
            {
                if (Data.Count > 0)
                    Data.RemoveItems(0, Data.Count);
            }
            else
            {
                Data = new SimpleDataHelper<TDataType>(this);
            }
            
            return pagesPaginatedRepository.LoadDataFromServer();
        }

        // This is called initially, as many times as needed to fill the viewport, 
        // and anytime the viewport's size grows, thus allowing more items to be displayed
        // Here you create the "ViewsHolder" instance whose views will be re-used
        // *For the method's full description check the base implementation
        protected override TViewPageViewHolder CreateViewsHolder(int itemIndex)
        {
            var instance = new TViewPageViewHolder();

            // Using this shortcut spares you from:
            // - instantiating the prefab yourself
            // - enabling the instance game object
            // - setting its index 
            // - calling its CollectViews()
            instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

            return instance;
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

            int lastVisibleItemItemIndex = -1;
            if (_VisibleItemsCount > 0)
            {
                lastVisibleItemItemIndex = _VisibleItems.Last().ItemIndex;
            }

            var numberOfItemsBelowLastVisible = Data.Count - (lastVisibleItemItemIndex + 1);

            // If the number of items available below the last visible (i.e. the bottom-most one, in our case) is less than <adapterParams.preFetchedItemsCount>, get more
            if (numberOfItemsBelowLastVisible < _Params.PreFetchedItemsCount)
            {
                uint newPotentialNumberOfItems = (uint) (Data.Count + _Params.PreFetchedItemsCount);
                if (TotalCapacity > -1) // i.e. the capacity isn't unlimited
                    newPotentialNumberOfItems = Math.Min(newPotentialNumberOfItems, TotalCapacity);

                if (newPotentialNumberOfItems > Data.Count) // i.e. if we there's enough room for at least 1 more item
                    await StartPreFetching((uint) (newPotentialNumberOfItems - Data.Count));
            }
        }

        private Task StartPreFetching(uint additionalItems)
        {
            _fetching = true;
            StartedFetching?.Invoke();
            return FetchItemModelsFromServer(additionalItems, OnPreFetchingFinished);
        }

        private Task FetchItemModelsFromServer(uint maxCount, Action<IReadOnlyList<TDataType>> onDone)
        {
            _asyncOperationCancellationController.CancelOngoingTask();
            return pagesPaginatedRepository.CreateGetItemsRangeTask(_retrievingItemsStartingIndex, maxCount)
                .ContinueWith(delegate(Task<IReadOnlyList<TDataType>> task)
                    {
                        _retrievingItemsStartingIndex += maxCount - 1;
                        onDone(task.Result);

                        _loadedAll = Data.Count == TotalCapacity;
                    }, _asyncOperationCancellationController.CancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnPreFetchingFinished(IReadOnlyList<TDataType> models)
        {
            if (models.Count > 0)
                Data.InsertItemsAtEnd(models as IList<TDataType>, _Params.FreezeContentEndEdgeOnCountChange);

            _fetching = false;
            EndedFetching?.Invoke();
        }

        // This is called anytime a previously invisible item become visible, or after it's created, 
        // or when anything that requires a refresh happens
        // Here you bind the data from the model to the item's views
        // *For the method's full description check the base implementation
        protected override async void UpdateViewsHolder(TViewPageViewHolder newOrRecycled)
        {
            // In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
            // index of item that should be represented by this views holder. You'll use this index
            // to retrieve the model from your data set
            try
            {
                var index = (uint) newOrRecycled.ItemIndex;
                await newOrRecycled.FillView(_fillingViewAdapter.Convert(Data[(int) index],index), index);
            }
            catch (OperationCanceledException e)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        #endregion

        // These are common data manipulation methods
        // The list containing the models is managed by you. The adapter only manages the items' sizes and the count
        // The adapter needs to be notified of any change that occurs in the data list. Methods for each
        // case are provided: Refresh, ResetItems, InsertItems, RemoveItems

        #region data manipulation

        public void AddItemsAt(int index, IList<TDataType> items)
        {
            // Commented: the below 2 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
            //YourList.InsertRange(index, items);
            //InsertItems(index, items.Length);

            Data.InsertItems(index, items);
        }

        public void RemoveItemsFrom(int index, int count)
        {
            // Commented: the below 2 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
            //YourList.RemoveRange(index, count);
            //RemoveItems(index, count);

            Data.RemoveItems(index, count);
        }

        public void SetItems(IList<TDataType> items)
        {
            // Commented: the below 3 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
            //YourList.Clear();
            //YourList.AddRange(items);
            //ResetItems(YourList.Count);

            Data.ResetItems(items);
        }

        #endregion

        // This class keeps references to an item's views.
        // Your views holder should extend BaseItemViewsHolder for ListViews and CellViewsHolder for GridViews
    }

    /*class MerchantInterestsListAdapter : RepositoryBasedListAdapter<>
    {
    }*/
}