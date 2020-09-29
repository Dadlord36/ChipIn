using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.TheFallenGames.OSA.Core;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Repositories.Interfaces;
using Repositories.Remote;
using Tasking;
using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Cards;
using ViewModels.Elements;
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
        TFillingViewAdapter> :
        BasedListAdapter<RepositoryPagesAdapterParameters, TViewPageViewHolder, TDataType, TViewConsumableData, TFillingViewAdapter>, IResettableAsync
        where TDataType : class
        where TViewConsumableData : class
        where TRepository : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
        where TViewPageViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>, new()
    {
        [SerializeField] private TRepository pagesPaginatedRepository;
        [SerializeField] private uint amountOfItemsAllowedToFetch;
        [SerializeField] private bool allowedToFetchAllItems = true;

        public UnityEvent startedFetching;
        public UnityEvent endedFetching;
        public UnityEvent showAllWasClicked;


        private bool _fetching;
        private bool _allItemsAreFetched;
        private uint _retrievingItemsStartingIndex;

        private uint TotalCapacity => pagesPaginatedRepository.TotalItemsNumber;

        public int SpecialItemIndex { get; set; }

        private uint AmountOfItemsAllowedToFetch
        {
            get => Math.Min(amountOfItemsAllowedToFetch, TotalCapacity);
            set => amountOfItemsAllowedToFetch = value;
        }

        [Binding]
        public bool AllItemsAreFetched
        {
            get => _allItemsAreFetched;
            set
            {
                if (value == _allItemsAreFetched) return;
                _allItemsAreFetched = value;
                OnPropertyChanged();
            }
        }

        private void ResetStateVariables()
        {
            _fetching = _allItemsAreFetched = false;
            _retrievingItemsStartingIndex = 0;
        }


        private bool DecideIfAlternativeFormShouldBeUsed(AbstractViewsHolder baseItemViewsHolder)
        {
            var switchableForm = GameObjectsUtility.GetFromRootOrChildren<SwitchableForm>(baseItemViewsHolder.root);
            return switchableForm && DecideIfAlternativeFormShouldBeUsed(switchableForm, baseItemViewsHolder);
        }

        private bool DecideIfAlternativeFormShouldBeUsed(SwitchableForm switchableForm, AbstractViewsHolder baseItemViewsHolder)
        {
            var state = Data[baseItemViewsHolder.ItemIndex] == null;
            switchableForm.AlternativeFormUsed = state;
            return state;
        }

        protected override BaseItemViewsHolder CreateViewsHolder(int itemIndex)
        {
            DecideScrollingIsAllowedOrNot();
            var instance = base.CreateViewsHolder(itemIndex);
            var switchableForm = GameObjectsUtility.GetFromRootOrChildren<SwitchableForm>(instance.root);

            if (!switchableForm) return instance;

            GameObjectsUtility.GetFromRootOrChildren<ShowAllItemsCallbackComponent>(instance.root).ShowAllItemsClicked += OnShowAllWasClicked;
            GameObjectsUtility.GetFromRootOrChildren<ItemsLeftViewModel>(instance.root).ItemsLeftNumber = TotalCapacity - AmountOfItemsAllowedToFetch;
            DecideIfAlternativeFormShouldBeUsed(switchableForm, instance);

            return instance;
        }

        protected override void UpdateViewsHolder(BaseItemViewsHolder viewHolder)
        {
            if (DecideIfAlternativeFormShouldBeUsed(viewHolder)) return;
            base.UpdateViewsHolder(viewHolder);
        }

        private void EqualizeAllowedItemsAmountToTotalCapacity()
        {
            AmountOfItemsAllowedToFetch = TotalCapacity;
        }

        private void ClearRemainListItems()
        {
            pagesPaginatedRepository.Clear();
            if (Data.Count > 0)
            {
                Data.RemoveItemsFromStart(Data.Count);
            }
            Refresh();
        }

        public async Task ResetAsync()
        {
            if (!IsInitialized)
            {
                Init();
            }

            ResetStateVariables();
            ClearRemainListItems();
            
            try
            {
                OnStartedFetching();
                await pagesPaginatedRepository.LoadDataFromServer().ConfigureAwait(false);
                LogUtility.PrintLog(Tag, $"List Items Total Capacity: {TotalCapacity.ToString()}");
                ItemsListIsNotEmpty = TotalCapacity > 0;

                if (ItemsListIsNotEmpty && allowedToFetchAllItems)
                {
                    EqualizeAllowedItemsAmountToTotalCapacity();
                }

                await FetchItemsAndRefillTheList().ConfigureAwait(false);
                OnEndedFetching();
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

        /*protected override async void OnInitialized()
        {
            base.OnInitialized();
            try
            {
                await ResetAsync();
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }*/

        private void SetInteractivity(bool state)
        {
            Parameters.SetScrollInteractivity(state);
        }

        protected override async void OnScrollPositionChanged(double normPos)
        {
            base.OnScrollPositionChanged(normPos);
            try
            {
                await UpdateList().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private async Task UpdateList()
        {
            DecideScrollingIsAllowedOrNot();

            if (!IsInitialized)
                return;

            if (_fetching)
                return;

            if (_allItemsAreFetched)
                return;

            if (Data == null)
                return;

            try
            {
                await FetchItemsAndRefillTheList().ConfigureAwait(false);
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

        private void DecideScrollingIsAllowedOrNot()
        {
            if (_VisibleItemsCount <= 0) return;
            SetInteractivity(TotalCapacity > _VisibleItems.Count);
        }

        private async Task FetchItemsAndRefillTheList()
        {
            int lastVisibleItemItemIndex = -1;

            if (_VisibleItemsCount > 0)
            {
                lastVisibleItemItemIndex = _VisibleItems.Last().ItemIndex;
            }

            var numberOfItemsBelowLastVisible = Data.Count - (lastVisibleItemItemIndex + 1);

            // If the number of items available below the last visible (i.e. the bottom-most one, in our case) is less than <adapterParams.preFetchedItemsCount>,
            // get more
            if (numberOfItemsBelowLastVisible >= _Params.PreFetchedItemsCount)
                return;
            var newPotentialNumberOfItems = (uint) (Data.Count + _Params.PreFetchedItemsCount);

            newPotentialNumberOfItems = Math.Min(newPotentialNumberOfItems, AmountOfItemsAllowedToFetch);

            if (newPotentialNumberOfItems <= Data.Count)
                return;

            try
            {
                _fetching = true;
                OnStartedFetching();
                await FetchItemModelsFromServerAsync((uint) (newPotentialNumberOfItems - Data.Count)).ConfigureAwait(false);
                LogUtility.PrintLog(Tag, "Fetching finished");
            }
            catch (ArgumentOutOfRangeException e)
            {
                LogUtility.PrintLog(Tag, e.Message);
            }
            finally
            {
                _fetching = false;
                OnEndedFetching();
            }
        }

        private async Task FetchItemModelsFromServerAsync(uint maxCount)
        {
            AsyncOperationCancellationController.CancelOngoingTask();
            var items = await pagesPaginatedRepository.GetItemsRangeAsync(_retrievingItemsStartingIndex, maxCount)
                .ConfigureAwait(false);
            _retrievingItemsStartingIndex += (uint) items.Count;
            AllItemsAreFetched = Data.Count == AmountOfItemsAllowedToFetch;

            if (items.Count > 0)
            {
                AddItemsAtTheEnd(items);
            }

            OnEndedFetching();
        }

        private void AddItemsAtTheEnd(IEnumerable<TDataType> items)
        {
            TasksFactories.ExecuteOnMainThread(() => { Data.InsertItemsAtEnd(items as IList<TDataType>, _Params.FreezeContentEndEdgeOnCountChange); });
        }

        /// <summary>
        /// Adds null item at the and, later to be used as marker for inserting special items at the end of list
        /// </summary>
        /// <returns>Index of null item in data array</returns>
        private int AddNullItemAtTheEnd()
        {
            TasksFactories.ExecuteOnMainThread(() => { Data.InsertOneAtEnd(null, _Params.FreezeContentEndEdgeOnCountChange); });
            return Data.Count;
        }

        private void OnStartedFetching()
        {
            TasksFactories.ExecuteOnMainThread(() => { startedFetching?.Invoke(); });
        }

        private void OnEndedFetching()
        {
            if (allowedToFetchAllItems)
            {
                TasksFactories.ExecuteOnMainThread(() => endedFetching?.Invoke());
            }
            else
            {
                TasksFactories.ExecuteOnMainThread(() =>
                {
                    if (Data.Count == AmountOfItemsAllowedToFetch && AmountOfItemsAllowedToFetch < TotalCapacity)
                    {
                        SpecialItemIndex = AddNullItemAtTheEnd();
                    }

                    endedFetching?.Invoke();
                });
            }
        }

        private void OnShowAllWasClicked()
        {
            showAllWasClicked.Invoke();
        }
    }
}