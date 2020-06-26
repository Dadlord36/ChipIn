using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Repositories.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Controllers.SlotsSpinningControllers.RecyclerView
{
    public interface IPaginatedRepositoryRecyclerViewAdapter
    {
        /// <summary>
        /// Initialize adapter
        /// </summary>
        /// <param name="itemsCouldBeShownAtATime">Amount of items, that should be visible on screen at the same time</param>
        /// <returns>Returns bool, meaning items are >= </returns>
        Task<AdapterInitializationResult> Initialize(uint itemsCouldBeShownAtATime);

        void OnSwiped(int itemsOutBorders);
    }


    public abstract class PaginatedRepositoryRecyclerViewAdapter<TView, TDataModel, TPaginatedRepository> : UIBehaviour,
        IPaginatedRepositoryRecyclerViewAdapter
        where TDataModel : class
        where TView : UIBehaviour, IFillingView<TDataModel>
        where TPaginatedRepository : IPaginatedItemsListRepository<TDataModel>
    {
        [SerializeField] private TView viewPrefab;
        [SerializeField] private TPaginatedRepository repository;
        
        private Transform ItemsContainerRoot => transform;
        private TView ViewPrefab => viewPrefab;
        private int ChildCount => ItemsContainerRoot.childCount;
        private Transform GetChild(int index) => ItemsContainerRoot.GetChild(index);


        #region Public Methods

        /// <summary>
        /// Initialize adapter
        /// </summary>
        /// <param name="itemsCouldBeShownAtATime">Amount of items, that should be visible on screen at the same time</param>
        /// <returns>Returns bool, meaning items are >= </returns>
        public async Task<AdapterInitializationResult> Initialize(uint itemsCouldBeShownAtATime)
        {
            ClearItems();
            try
            {
                var dataItems = await repository.CreateGetItemsRangeTask(0, itemsCouldBeShownAtATime);
                allItemsCount = repository.TotalItemsNumber;
                itemsInCycleCount = dataItems.Count;
                FillWithItems(itemsInCycleCount);
                await UpdateContent();
                return new AdapterInitializationResult(allItemsCount, (uint) itemsInCycleCount,
                    itemsCouldBeShownAtATime >= itemsInCycleCount);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private List<TView> views = new List<TView>();
        private uint allItemsCount;
        private int startingIndex=0;
        private int itemsInCycleCount;

        void SwitchViewsByIndexes(int from, int to)
        {
            var value = views[from];
            views[from] = views[to];
            views[to] = value;
        }


        public async void OnSwiped(int itemsOutBorders)
        {
            var absoluteAmount = Math.Abs(itemsOutBorders);
            startingIndex += itemsOutBorders;
            // IList<uint> keys;

            // Forward movement case
            if (itemsOutBorders > 0)
            {
                // keys = GetPairKeys(GetItemsFromBottomInReverse(absoluteAmount));
            }
            // Reverse movement case
            else
            {
                // keys = GetPairKeys(GetItemsFromBottomInReverse(absoluteAmount));
            }

            // ShiftItemsIndexes(keys, itemsOutBorders);

            /*for (int i = 0; i < selectedIndexes.Length; i++)
            {
                indexes[selectedIndexes[i]] += itemsOutBorders;
            }*/

            try
            {
                await UpdateContent();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        #endregion

        private void FillWithItems(int itemsAmount)
        {
            EvenInstantiatedItemsToNumber(itemsAmount);
        }

        private async Task UpdateContent()
        {
            /*var tasks = new List<Task>();
            foreach (var keyValuePair in _fillingViewsDictionary)
            {
                tasks.Add(OnBindViewHolder(keyValuePair));
            }

            return Task.WhenAll(tasks);*/

            int GetIndexCycled(int index)
            {
                return (int) Mathf.Repeat(index,itemsInCycleCount-1);
            }

            for (int i = startingIndex; i < allItemsCount; i++)
            {
                await OnBindViewHolder(GetIndexCycled(i), (uint)i);
   
            }
            
            
            /*foreach (var keyValuePair in _fillingViewsDictionary)
            {
                await OnBindViewHolder(keyValuePair);
            }*/
        }

        private async Task OnBindViewHolder(int viewIndex, uint dataIndex)
        {
            try
            {
                var result = await repository.CreateGetItemWithIndexTask(dataIndex);
                views[viewIndex].FillView(result, dataIndex);
            }
            catch (ArgumentOutOfRangeException e)
            {
                LogUtility.PrintLog(GetType().Name, e.Message);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        /*private async Task OnBindViewHolder(KeyValuePair<uint, TView> dataIndexFiller)
        {
            try
            {
                dataIndexFiller.Value.FillView(await repository.CreateGetItemWithIndexTask(dataIndexFiller.Key), dataIndexFiller.Key);
            }
            catch (ArgumentOutOfRangeException e)
            {
                LogUtility.PrintLog(GetType().Name, e.Message);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }

            LogUtility.PrintLog(GetType().Name, $"Index = {dataIndexFiller.Key.ToString()}");
        }*/

        /*private void ShiftItemsIndexes(IList<uint> items, int indexShift)
        {
            for (int i = 0; i < items.Count; i++)
            {
                ChangeDictionaryItemKey(items[i], (uint) (items[i] + indexShift));
            }
        }

        private void ChangeDictionaryItemKey(uint from, uint to)
        {
            ChangeDictionaryItemKey(_fillingViewsDictionary, from, to);
        }*/

        private void EvenInstantiatedItemsToNumber(int number)
        {
            var difference = number - ChildCount;

            if (difference == 0) return;
            var absoluteDifference = Mathf.Abs(difference);

            if (difference > 0)
            {
                for (uint i = 0; i < absoluteDifference; i++)
                {
                    views.Add(OnCreateViewHolder());
                }
            }
            else
            {
                for (int i = ItemsContainerRoot.childCount - 1; i >= ItemsContainerRoot.childCount - number; i--)
                {
                    Destroy(ItemsContainerRoot.GetChild(i).gameObject);
                    views.RemoveAt(i);
                }
            }

            views.TrimExcess();
        }

        private TView OnCreateViewHolder()
        {
            return Instantiate(ViewPrefab, ItemsContainerRoot);
        }

        /*private void CreateAndAttachOneItem(uint index)
        {
            _fillingViewsDictionary.Add(index, OnCreateViewHolder());
        }*/

        /*private IList<TView> GetViewsFromTop(int numberOfCards)
        {
            /*
                 for (int i = 0; i < numberOfCards; i++)
                {
                    array[i] = GetChild(i).GetComponent<TView>();
                }#1#

            return GetPairValues(GetItemsFromTopInReverse(numberOfCards));
        }*/

        /*private IList<TView> GetViewsFromBottom(int numberOfCards)
        {
            /*int startIndex = (int) (ChildCount - numberOfCards);
                for (int i = 0; i < numberOfCards; i++)
                {
                    array[i] = GetChild(i + startIndex).GetComponent<TView>();
                }#1#

            return GetPairValues(GetItemsFromBottomInReverse(numberOfCards));
        }*/

        /*private IReadOnlyList<KeyValuePair<uint, TView>> GetItemsFromTopInReverse(int numberOfItems)
        {
            return GetFirstItemsInReverse(_fillingViewsDictionary, numberOfItems);
        }

        private IReadOnlyList<KeyValuePair<uint, TView>> GetItemsFromBottomInReverse(int numberOfItems)
        {
            return GetLastItemsInReverse(_fillingViewsDictionary, numberOfItems);
        }*/

        /*private static IList<TValue> GetPairValues<TKey, TValue>(IReadOnlyList<KeyValuePair<TKey, TValue>> source)
        {
            var array = new TValue[source.Count];
            for (int i = 0; i < source.Count; i++)
            {
                array[i] = source[i].Value;
            }

            return array;
        }

        private static IList<TKey> GetPairKeys<TKey, TValue>(IReadOnlyList<KeyValuePair<TKey, TValue>> source)
        {
            var array = new TKey[source.Count];
            for (int i = 0; i < source.Count; i++)
            {
                array[i] = source[i].Key;
            }

            return array;
        }*/

        private void ClearItems()
        {
            GameObjectsUtility.DestroyTransformAttachments(ItemsContainerRoot);
            views.Clear();
            /*_fillingViewsDictionary.Clear();*/
        }

        /*private static void ChangeDictionaryItemKey<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey from, TKey to)
        {
            var value = dictionary[from];
            dictionary.Remove(from);
            dictionary.Add(to, value);
        }*/

        /*private static KeyValuePair<TKey, TValue>[] GetFirstItemsInReverse<TKey, TValue>(IDictionary<TKey, TValue> dictionary, int itemsNumber)
        {
            var firstItems = new KeyValuePair<TKey, TValue>[itemsNumber];
            var array = dictionary.ToArray();

            for (int i = itemsNumber; i >= 0; i--)
            {
                firstItems[i] = array[i];
            }

            return firstItems;
        }*/

        /*private static KeyValuePair<TKey, TValue>[] GetLastItemsInReverse<TKey, TValue>(IDictionary<TKey, TValue> dictionary, int itemsNumber)
        {
            var lastItems = new KeyValuePair<TKey, TValue>[itemsNumber];
            var array = dictionary.ToArray();

            int index = 0;
            for (int i = array.Length - 1; i >= array.Length - itemsNumber; i--)
            {
                lastItems[index] = array[i];
                index++;
            }

            return lastItems;
        }*/
    }
}