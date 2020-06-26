/*
 * * * * This bare-bones script was auto-generated * * * *
 * The code commented with "/ * * /" demonstrates how data is retrieved and passed to the adapter, plus other common commands. You can remove/replace it once you've got the idea
 * Complete it according to your specific use-case
 * Consult the Example scripts if you get stuck, as they provide solutions to most common scenarios
 * 
 * Main terms to understand:
 *		Model = class that contains the data associated with an item (title, content, icon etc.)
 *		Views Holder = class that contains references to your views (Text, Image, MonoBehavior, etc.)
 * 
 * Default expected UI hiererchy:
 *	  ...
 *		-Canvas
 *		  ...
 *			-MyScrollViewAdapter
 *				-Viewport
 *					-Content
 *				-Scrollbar (Optional)
 *				-ItemPrefab (Optional)
 * 
 * Note: If using Visual Studio and opening generated scripts for the first time, sometimes Intellisense (autocompletion)
 * won't work. This is a well-known bug and the solution is here: https://developercommunity.visualstudio.com/content/problem/130597/unity-intellisense-not-working-after-creating-new-1.html (or google "unity intellisense not working new script")
 * 
 * 
 * Please read the manual under "Assets/OSA/Docs", as it contains everything you need to know in order to get started, including FAQ
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using Repositories.Remote.Paginated;
using UnityEngine;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters.Parameters;
using Views.ViewElements.ScrollViews.Adapters.Parameters.Interfaces;

// You should modify the namespace to your own or - if you're sure there won't ever be conflicts - remove it altogether
namespace Views.ViewElements.ScrollViews.Adapters
{
    // There are 2 important callbacks you need to implement, apart from Start(): CreateViewsHolder() and UpdateViewsHolder()
    // See explanations below
    public class UserInterestPagesListAdapter : OSA<UserInterestPagesListAdapter.UserInterestPagesAdapterParameters, UserInterestPageViewHolder>
    {
        [Serializable]
        public class UserInterestPagesAdapterParameters : BaseParamsWithPrefab, IRepositoryAdapterParameters
        {
            public RepositoryAdapterParameters repositoryAdapterParameters;
            public int PreFetchedItemsCount => repositoryAdapterParameters.PreFetchedItemsCount;

            public bool FreezeContentEndEdgeOnCountChange => repositoryAdapterParameters.FreezeContentEndEdgeOnCountChange;
        }

        [SerializeField] private UserInterestPagesPaginatedRepository userInterestPagesPaginatedRepository;
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        public event Action StartedFetching;
        public event Action EndedFetching;

        // Helper that stores data and notifies the adapter when items count changes
        // Can be iterated and can also have its elements accessed by the [] operator
        public SimpleDataHelper<InterestPagePageDataModel> Data { get; private set; }
        private uint TotalCapacity => userInterestPagesPaginatedRepository.TotalItemsNumber;

        #region OSA implementation

        private bool fetching;
        private bool loadedAll;
        private uint _retrievingItemsStartingIndex;

        private void ResetStateVariables()
        {
            fetching = loadedAll = false;
            _retrievingItemsStartingIndex = 0;
        }

        public Task Initialize()
        {
            ResetStateVariables();
            if (Data != null)
            {
                Data.RemoveItems(0, Data.Count);
            }
            else
            {
                Data = new SimpleDataHelper<InterestPagePageDataModel>(this);
            }

            return userInterestPagesPaginatedRepository.LoadDataFromServer();
        }

        // This is called initially, as many times as needed to fill the viewport, 
        // and anytime the viewport's size grows, thus allowing more items to be displayed
        // Here you create the "ViewsHolder" instance whose views will be re-used
        // *For the method's full description check the base implementation
        protected override UserInterestPageViewHolder CreateViewsHolder(int itemIndex)
        {
            var instance = new UserInterestPageViewHolder();

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

            if (fetching)
                return;

            if (loadedAll)
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
            fetching = true;
            StartedFetching?.Invoke();
            return FetchItemModelsFromServer(additionalItems, OnPreFetchingFinished);
        }

        private Task FetchItemModelsFromServer(uint maxCount, Action<IReadOnlyList<InterestPagePageDataModel>> onDone)
        {
            _asyncOperationCancellationController.CancelOngoingTask();
            return userInterestPagesPaginatedRepository.CreateGetItemsRangeTask(_retrievingItemsStartingIndex, maxCount)
                .ContinueWith(delegate(Task<IReadOnlyList<InterestPagePageDataModel>> task)
                    {
                        _retrievingItemsStartingIndex += maxCount - 1;
                        onDone(task.Result);

                        loadedAll = Data.Count == TotalCapacity;
                    }, _asyncOperationCancellationController.CancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnPreFetchingFinished(IReadOnlyList<InterestBasicDataModel> models)
        {
            if (models.Count > 0)
                Data.InsertItemsAtEnd(models as IList<InterestPagePageDataModel>, _Params.FreezeContentEndEdgeOnCountChange);

            fetching = false;
            EndedFetching?.Invoke();
        }

        // This is called anytime a previously invisible item become visible, or after it's created, 
        // or when anything that requires a refresh happens
        // Here you bind the data from the model to the item's views
        // *For the method's full description check the base implementation
        protected override async void UpdateViewsHolder(UserInterestPageViewHolder newOrRecycled)
        {
            // In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
            // index of item that should be represented by this views holder. You'll use this index
            // to retrieve the model from your data set
            try
            {
                await newOrRecycled.FillView(Data[newOrRecycled.ItemIndex], (uint) newOrRecycled.ItemIndex);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        // This is the best place to clear an item's views in order to prepare it from being recycled, but this is not always needed, 
        // especially if the views' values are being overwritten anyway. Instead, this can be used to, for example, cancel an image 
        // download request, if it's still in progress when the item goes out of the viewport.
        // <newItemIndex> will be non-negative if this item will be recycled as opposed to just being disabled
        // *For the method's full description check the base implementation
        /*
        protected override void OnBeforeRecycleOrDisableViewsHolder(MyListItemViewsHolder inRecycleBinOrVisible, int newItemIndex)
        {
            base.OnBeforeRecycleOrDisableViewsHolder(inRecycleBinOrVisible, newItemIndex);
        }
        */

        // You only need to care about this if changing the item count by other means than ResetItems, 
        // case in which the existing items will not be re-created, but only their indices will change.
        // Even if you do this, you may still not need it if your item's views don't depend on the physical position 
        // in the content, but they depend exclusively to the data inside the model (this is the most common scenario).
        // In this particular case, we want the item's index to be displayed and also to not be stored inside the model,
        // so we update its title when its index changes. At this point, the Data list is already updated and 
        // shiftedViewsHolder.ItemIndex was correctly shifted so you can use it to retrieve the associated model
        // Also check the base implementation for complementary info
        /*
        protected override void OnItemIndexChangedDueInsertOrRemove(MyListItemViewsHolder shiftedViewsHolder, int oldIndex, bool wasInsert, int removeOrInsertIndex)
        {
            base.OnItemIndexChangedDueInsertOrRemove(shiftedViewsHolder, oldIndex, wasInsert, removeOrInsertIndex);

            shiftedViewsHolder.titleText.text = Data[shiftedViewsHolder.ItemIndex].title + " #" + shiftedViewsHolder.ItemIndex;
        }
        */

        #endregion

        // These are common data manipulation methods
        // The list containing the models is managed by you. The adapter only manages the items' sizes and the count
        // The adapter needs to be notified of any change that occurs in the data list. Methods for each
        // case are provided: Refresh, ResetItems, InsertItems, RemoveItems

        #region data manipulation

        public void AddItemsAt(int index, IList<InterestPagePageDataModel> items)
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

        public void SetItems(IList<InterestPagePageDataModel> items)
        {
            // Commented: the below 3 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
            //YourList.Clear();
            //YourList.AddRange(items);
            //ResetItems(YourList.Count);

            Data.ResetItems(items);
        }

        #endregion
    }

    // This class keeps references to an item's views.
    // Your views holder should extend BaseItemViewsHolder for ListViews and CellViewsHolder for GridViews
    public class UserInterestPageViewHolder : BaseItemViewsHolder, IFillingView<InterestPagePageDataModel>
    {
        private const string Tag = nameof(UserInterestPageViewHolder);
        private IFillingView<InterestPagePageDataModel> fillingViewImplementation;

        // Retrieving the views from the item's root GameObject
        public override void CollectViews()
        {
            base.CollectViews();

            // GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
            // which infers the variable's component from its type, so you won't need to specify it yourself
            if (!root.TryGetComponent(out fillingViewImplementation))
            {
                LogUtility.PrintLogError(Tag, $"{root.name} has no attached component of type {nameof(UserInterestPageViewHolder)}");
            }
        }

        // Override this if you have children layout groups or a ContentSizeFitter on root that you'll use. 
        // They need to be marked for rebuild when this callback is fired
        /*
        public override void MarkForRebuild()
        {
            base.MarkForRebuild();

            LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout1);
            LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout2);
            YourSizeFitterOnRoot.enabled = true;
        }
        */

        // Override this if you've also overridden MarkForRebuild() and you have enabled size fitters there (like a ContentSizeFitter)
        /*
        public override void UnmarkForRebuild()
        {
            YourSizeFitterOnRoot.enabled = false;

            base.UnmarkForRebuild();
        }
        */
        public async Task FillView(InterestPagePageDataModel dataModel, uint dataBaseIndex)
        {
            await fillingViewImplementation.FillView(dataModel, dataBaseIndex);
        }
    }
}