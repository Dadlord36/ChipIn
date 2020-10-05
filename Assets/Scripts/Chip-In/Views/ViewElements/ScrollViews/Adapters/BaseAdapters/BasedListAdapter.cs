using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using Common.Interfaces;
using Common.UnityEvents;
using Controllers;
using Controllers.SlotsSpinningControllers.RecyclerView.Interfaces;
using Factories;
using JetBrains.Annotations;
using Repositories.Local;
using Tasking;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements.ScrollViews.Adapters.ViewFillingAdapters;

namespace Views.ViewElements.ScrollViews.Adapters.BaseAdapters
{
    [Binding]
    public abstract class BasedListAdapter<TParams, TItemViewHolder, TDataType, TViewConsumableData, TFillingViewAdapter> :
        OSA<TParams, BaseItemViewsHolder>,
        INotifyPropertyChanged
        where TItemViewHolder : BaseItemViewsHolder, IFillingView<TViewConsumableData>, new()
        where TParams : BaseParamsWithPrefab
        where TViewConsumableData : class
        where TFillingViewAdapter : FillingViewAdapter<TDataType, TViewConsumableData>, new()
    {
        protected readonly string Tag;

        private IDownloadedSpritesRepository downloadedSpritesRepository => SimpleAutofac.GetInstance<IDownloadedSpritesRepository>();
        public BoolUnityEvent listFillingStateChanged;

        // Helper that stores data and notifies the adapter when items count changes
        // Can be iterated and can also have its elements accessed by the [] operator
        protected SimpleDataHelper<TDataType> Data;
        private readonly TFillingViewAdapter _fillingViewAdapter = new TFillingViewAdapter();
        protected readonly AsyncOperationCancellationController AsyncOperationCancellationController = new AsyncOperationCancellationController();
        private bool _itemsListIsNotEmpty = true;
        protected int MiddleElementNumber;
        private BaseItemViewsHolder _middleItem;


        [Binding]
        public bool ItemsListIsNotEmpty
        {
            get => _itemsListIsNotEmpty;
            set
            {
                _itemsListIsNotEmpty = value;
                OnPropertyChanged();
                OnListFillingStateChanged(value);
            }
        }

        /// <summary>
        /// Middle item in scroll viewport. Will also call Select() on new middle item sets
        /// </summary>
        protected BaseItemViewsHolder MiddleItem
        {
            get => _middleItem;
            set
            {
                if (ReferenceEquals(_middleItem, value)) return;
                _middleItem = value;
                (value as IIdentifiedSelection).Select();
            }
        }

        public BasedListAdapter()
        {
            Tag = GetType().Name;
        }

        protected override void Awake()
        {
            base.Awake();
            Data = new SimpleDataHelper<TDataType>(this);
        }

        protected virtual void AdditionItemProcessing(BaseItemViewsHolder viewHolder, int itemIndex)
        {
        }

        // This is called initially, as many times as needed to fill the viewport, 
        // and anytime the viewport's size grows, thus allowing more items to be displayed
        // Here you create the "ViewsHolder" instance whose views will be re-used
        // *For the method's full description check the base implementation
        protected override BaseItemViewsHolder CreateViewsHolder(int itemIndex)
        {
            var instance = new TItemViewHolder();

            // Using this shortcut spares you from:
            // - instantiating the prefab yourself
            // - enabling the instance game object
            // - setting its index 
            // - calling its CollectViews()
            instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
            AdditionItemProcessing(instance, itemIndex);
            return instance;
        }

        // This is called anytime a previously invisible item become visible, or after it's created, 
        // or when anything that requires a refresh happens
        // Here you bind the data from the model to the item's views
        // *For the method's full description check the base implementation
        protected override async void UpdateViewsHolder(BaseItemViewsHolder viewHolder)
        {
            // In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
            // index of item that should be represented by this views holder. You'll use this index
            // to retrieve the model from your data set
            try
            {
                var index = (uint) viewHolder.ItemIndex;
                var data = _fillingViewAdapter.Convert
                (
                    AsyncOperationCancellationController.TasksCancellationTokenSource,
                    Data[(int) index],
                    index
                );
                if (viewHolder is IFillingView<TViewConsumableData> fillingView)
                {
                    await fillingView.FillView(data, index).ConfigureAwait(false);
                }
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

            TasksFactories.ExecuteOnMainThread(() => Data.InsertItems(index, items));
        }

        public void RemoveItemsFrom(int index, int count)
        {
            // Commented: the below 2 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
            //YourList.RemoveRange(index, count);
            //RemoveItems(index, count);

            TasksFactories.ExecuteOnMainThread(() => Data.RemoveItems(index, count));
        }

        public virtual void SetItems(IList<TDataType> items)
        {
            // Commented: the below 3 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
            //YourList.Clear();
            //YourList.AddRange(items);
            //ResetItems(YourList.Count);

            TasksFactories.ExecuteOnMainThread(() => Data.ResetItems(items));
        }

        #endregion


        protected void FindMiddleElement()
        {
            MiddleElementNumber = CalculationsUtility.GetMiddle(VisibleItemsCount);
            MiddleItem = _VisibleItems[MiddleElementNumber];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
        
        private void OnListFillingStateChanged(bool state)
        {
            TasksFactories.ExecuteOnMainThread(() => { listFillingStateChanged.Invoke(state); });
        }
    }
}