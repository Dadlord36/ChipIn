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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.TheFallenGames.OSA.CustomAdapters.GridView;
using Com.TheFallenGames.OSA.DataHelpers;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using DataModels;
using Repositories.Remote.Paginated;
using UnityEngine;
using Utilities;
using Views;

// You should modify the namespace to your own or - if you're sure there won't ever be conflicts - remove it altogether
namespace Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Grids
{

    // There is 1 important callback you need to implement, apart from Start(): UpdateCellViewsHolder()
    // See explanations below
    public class UserInterestsLabelsGridAdapter : GridAdapter<UserInterestsLabelsGridAdapter.UserInterestsLabelsGridParams,
        UserInterestLabelGridItemViewsHolder>
    {
        [Serializable]
        public class UserInterestsLabelsGridParams : GridParams
        {
            [SerializeField] private int preFetchedItemsCount;
            [NonSerialized] public bool FreezeContentEndEdgeOnCountChange;

            public int PreFetchedItemsCount => preFetchedItemsCount;
        }

        [SerializeField] private UserInterestsBasicDataPaginatedListRepository userInterestsBasicDataPaginatedListRepository;

        public event Action StartedFetching;
        public event Action EndedFetching;
        public event Action<int?> NewInterestSelected; 

        // Helper that stores data and notifies the adapter when items count changes
        // Can be iterated and can also have its elements accessed by the [] operator
        public SimpleDataHelper<InterestBasicDataModel> Data { get; private set; }
        private uint TotalCapacity => userInterestsBasicDataPaginatedListRepository.TotalItemsNumber;
        
        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();

        private bool fetching;
        private bool loadedAll;
        private uint _retrievingItemsStartingIndex;

        private void ResetStateVariables()
        {
            fetching = loadedAll = false;
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
                Data = new SimpleDataHelper<InterestBasicDataModel>(this);
            }
            return userInterestsBasicDataPaginatedListRepository.LoadDataFromServer(); 
        }


        #region GridAdapter implementation

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
                lastVisibleItemItemIndex = _VisibleItems.Last().ContainingCellViewsHolders.Last().ItemIndex;
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

        protected override CellGroupViewsHolder<UserInterestLabelGridItemViewsHolder> CreateViewsHolder(int itemIndex)
        {
            var viewsHolder = base.CreateViewsHolder(itemIndex);
            foreach (var holder in viewsHolder.ContainingCellViewsHolders)
            {
                holder.root.GetComponent<UserInterestGridItemView>().ItemSelected += OnNewInterestSelected;
            }
            return viewsHolder;
        }

        private void OnNewInterestSelected(int? index)
        {
            NewInterestSelected?.Invoke(index);
        }

        // Setting _Fetching to true & starting to fetch 
        private Task StartPreFetching(uint additionalItems)
        {
            fetching = true;
            StartedFetching?.Invoke();
            return FetchItemModelsFromServer(additionalItems, OnPreFetchingFinished);
        }

        private Task FetchItemModelsFromServer(uint maxCount, Action<IReadOnlyList<InterestBasicDataModel>> onDone)
        {
            _asyncOperationCancellationController.CancelOngoingTask();
            return userInterestsBasicDataPaginatedListRepository.CreateGetItemsRangeTask(_retrievingItemsStartingIndex, maxCount)
                .ContinueWith(delegate(Task<IReadOnlyList<InterestBasicDataModel>> task)
                {
                    _retrievingItemsStartingIndex += maxCount - 1;
                    onDone(task.Result);
                    
                    loadedAll = Data.Count == TotalCapacity;
                },_asyncOperationCancellationController.CancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, 
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnPreFetchingFinished(IReadOnlyList<InterestBasicDataModel> models)
        {
            if (models.Count > 0)
                Data.InsertItemsAtEnd(models as IList<InterestBasicDataModel>, _Params.FreezeContentEndEdgeOnCountChange);

            fetching = false;
            EndedFetching?.Invoke();
        }

        // This is called anytime a previously invisible item become visible, or after it's created, 
        // or when anything that requires a refresh happens
        // Here you bind the data from the model to the item's views
        // *For the method's full description check the base implementation
        protected override void UpdateCellViewsHolder(UserInterestLabelGridItemViewsHolder newOrRecycled)
        {
            // In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
            // index of item that should be represented by this views holder. You'll use this index
            // to retrieve the model from your data set
            newOrRecycled.FillView(Data[newOrRecycled.ItemIndex], (uint) newOrRecycled.ItemIndex);
        }

        // This is the best place to clear an item's views in order to prepare it from being recycled, but this is not always needed, 
        // especially if the views' values are being overwritten anyway. Instead, this can be used to, for example, cancel an image 
        // download request, if it's still in progress when the item goes out of the viewport.
        // <newItemIndex> will be non-negative if this item will be recycled as opposed to just being disabled
        // *For the method's full description check the base implementation
        /*
        protected override void OnBeforeRecycleOrDisableCellViewsHolder(MyGridItemViewsHolder inRecycleBinOrVisible, int newItemIndex)
        {
            base.OnBeforeRecycleOrDisableCellViewsHolder(inRecycleBinOrVisible, newItemIndex);
        }
        */

        #endregion

        // These are common data manipulation methods
        // The list containing the models is managed by you. The adapter only manages the items' sizes and the count
        // The adapter needs to be notified of any change that occurs in the data list. 
        // For GridAdapters, only Refresh and ResetItems work for now

        #region data manipulation

        public void AddItemsAt(int index, IList<InterestBasicDataModel> items)
        {
            //Commented: this only works with Lists. ATM, Insert for Grids only works by manually changing the list and calling NotifyListChangedExternally() after
            //Data.InsertItems(index, items);
            Data.List.InsertRange(index, items);
            Data.NotifyListChangedExternally();
        }

        public void RemoveItemsFrom(int index, int count)
        {
            //Commented: this only works with Lists. ATM, Remove for Grids only works by manually changing the list and calling NotifyListChangedExternally() after
            //Data.RemoveRange(index, count);
            Data.List.RemoveRange(index, count);
            Data.NotifyListChangedExternally();
        }

        public void SetItems(IList<InterestBasicDataModel> items)
        {
            Data.ResetItems(items);
        }

        #endregion
    }


    // This class keeps references to an item's views.
    // Your views holder should extend BaseItemViewsHolder for ListViews and CellViewsHolder for GridViews
    // The cell views holder should have a single child (usually named "Views"), which contains the actual 
    // UI elements. A cell's root is never disabled - when a cell is removed, only its "views" GameObject will be disabled
    public class UserInterestLabelGridItemViewsHolder : CellViewsHolder, IFillingView<InterestBasicDataModel>
    {
        private const string Tag = nameof(UserInterestLabelGridItemViewsHolder);
        private IFillingView<InterestBasicDataModel> _fillingViewImplementation;


        // Retrieving the views from the item's root GameObject
        public override void CollectViews()
        {
            base.CollectViews();

            // GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
            // which infers the variable's component from its type, so you won't need to specify it yourself
            if (!root.TryGetComponent(out _fillingViewImplementation))
            {
                LogUtility.PrintLogError(Tag, $"{root.name} has no component of type {nameof(UserInterestLabelGridItemViewsHolder)}");
            }
        }

        public Task FillView(InterestBasicDataModel dataModel, uint dataBaseIndex)
        {
            return _fillingViewImplementation.FillView(dataModel, dataBaseIndex);
        }

        // This is usually the only child of the item's root and it's called "Views". 
        // That's what the default implementation will look for, but just for flexibility, 
        // this callback is provided, in case it's named differently or there's more than 1 child 
        // *See GridExample.cs for more info
        /*
        protected override RectTransform GetViews()
        { return root.Find("Views").transform as RectTransform; }
        */

        // Override this if you have children layout groups. They need to be marked for rebuild when this callback is fired
        /*
        public override void MarkForRebuild()
        {
            base.MarkForRebuild();

            LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout1);
            LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout2);
            AChildSizeFitter.enabled = true;
        }
        */

        // Override this if you've also overridden MarkForRebuild()
        /*
        public override void UnmarkForRebuild()
        {
            AChildSizeFitter.enabled = false;

            base.UnmarkForRebuild();
        }
        */
    }
}