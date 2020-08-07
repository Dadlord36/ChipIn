using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.DataHelpers;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using JetBrains.Annotations;
using Repositories.Interfaces;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters.Parameters;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class RepositoryBasedListAdapter<TRepository, TDataType, TViewPageViewHolder, TViewConsumableData,
        TFillingViewAdapter> :
        OSA<RepositoryPagesAdapterParameters, TViewPageViewHolder>, INotifyPropertyChanged
        where TDataType : class
        where TViewConsumableData : class
        where TRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
        where TViewPageViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>, new()
    {
        private readonly string Tag;

        [SerializeField] private TRepository pagesPaginatedRepository;
        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;

        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        private readonly TFillingViewAdapter _fillingViewAdapter = new TFillingViewAdapter();

        public event Action StartedFetching;
        public event Action EndedFetching;

        [Binding]
        public bool ItemsListIsEmpty
        {
            get => _itemsListIsEmpty;
            private set
            {
                if (value == _itemsListIsEmpty) return;
                _itemsListIsEmpty = value;
                OnPropertyChanged();
            }
        }

        public RepositoryBasedListAdapter()
        {
            Tag = GetType().Name;
        }


        // Helper that stores data and notifies the adapter when items count changes
        // Can be iterated and can also have its elements accessed by the [] operator
        [Binding] protected SimpleDataHelper<TDataType> Data { get; set; }
        private uint TotalCapacity => pagesPaginatedRepository.TotalItemsNumber;

        #region OSA implementation

        private bool _fetching;
        private bool _loadedAll;
        private uint _retrievingItemsStartingIndex;
        private bool _itemsListIsEmpty = true;

        private void ResetStateVariables()
        {
            _fetching = _loadedAll = false;
            _retrievingItemsStartingIndex = 0;
        }

        public Task Initialize()
        {
            ResetStateVariables();
            _fillingViewAdapter.SetDownloadingSpriteRepository(downloadedSpritesRepository);

            if (Data != null)
            {
                if (Data.Count > 0)
                {
                    Data.RemoveItemsFromStart(Data.Count);
                }
            }
            else
            {
                Data = new SimpleDataHelper<TDataType>(this);
            }


            pagesPaginatedRepository.Clear();
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

            if (Data == null)
                return;

            int lastVisibleItemItemIndex = -1;
            if (_VisibleItemsCount > 0)
            {
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
                await StartPreFetchingAsync((uint) (newPotentialNumberOfItems - Data.Count)).ConfigureAwait(false);
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
            StartedFetching?.Invoke();
            return FetchItemModelsFromServerAsync(additionalItems);
        }

        private async Task FetchItemModelsFromServerAsync(uint maxCount)
        {
            _asyncOperationCancellationController.CancelOngoingTask();
            var items = await pagesPaginatedRepository.CreateGetItemsRangeTask(_retrievingItemsStartingIndex, maxCount)
                .ConfigureAwait(true);
            _retrievingItemsStartingIndex += (uint) items.Count;
            _loadedAll = Data.Count == TotalCapacity;
            OnPreFetchingFinished(items);
        }

        private void OnPreFetchingFinished(IReadOnlyCollection<TDataType> models)
        {
            if (models.Count > 0)
                Data.InsertItemsAtEnd(models as IList<TDataType>, _Params.FreezeContentEndEdgeOnCountChange);

            ItemsListIsEmpty = Data.Count == 0;

            EndedFetching?.Invoke();
        }

        // This is called anytime a previously invisible item become visible, or after it's created, 
        // or when anything that requires a refresh happens
        // Here you bind the data from the model to the item's views
        // *For the method's full description check the base implementation
        protected override async void UpdateViewsHolder(TViewPageViewHolder viewHolder)
        {
            // In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
            // index of item that should be represented by this views holder. You'll use this index
            // to retrieve the model from your data set
            try
            {
                var index = (uint) viewHolder.ItemIndex;
                var data = _fillingViewAdapter.Convert
                (
                    _asyncOperationCancellationController.TasksCancellationTokenSource,
                    Data[(int) index],
                    index
                );
                await viewHolder.FillView(data, index).ConfigureAwait(false);
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
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}