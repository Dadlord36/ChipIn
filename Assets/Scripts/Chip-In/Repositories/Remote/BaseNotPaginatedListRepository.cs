using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Common;

namespace Repositories.Remote
{
    public abstract class BaseNotPaginatedListRepository<TDataType> : BseItemsListRemoteRepository<TDataType>, INotifyCollectionChanged
    {
        [NonSerialized] protected LiveData<TDataType> ItemsLiveData = new LiveData<TDataType>();
        public override IReadOnlyList<TDataType> ItemsData => ItemsLiveData.DataList;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => ItemsLiveData.CollectionChanged += value;
            remove => ItemsLiveData.CollectionChanged -= value;
        }
    }
}