using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Common;
using Controllers;

namespace Repositories.Remote
{
    public abstract class BaseNotPaginatedListRepository<TDataType> : BseItemsListRemoteRepository<TDataType>, INotifyCollectionChanged
    {
        [NonSerialized] protected LiveData<TDataType> ItemsLiveData = new LiveData<TDataType>();

        private readonly AsyncOperationCancellationController _asyncOperationCancellationController = new AsyncOperationCancellationController();
        protected ref CancellationTokenSource TasksCancellationTokenSource => ref _asyncOperationCancellationController.TasksCancellationTokenSource;

        public override IReadOnlyList<TDataType> ItemsData => ItemsLiveData.DataList;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => ItemsLiveData.CollectionChanged += value;
            remove => ItemsLiveData.CollectionChanged -= value;
        }
        
        protected void CancelOngoingTask()
        {
            _asyncOperationCancellationController.CancelOngoingTask();
        }

        protected virtual void OnDisable()
        {
            CancelOngoingTask();
        }
    }
}