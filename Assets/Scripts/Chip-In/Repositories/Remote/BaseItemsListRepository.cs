using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Common;

namespace Repositories.Remote
{
    public abstract class BaseItemsListRepository<TDataType> : RemoteRepositoryBase, INotifyCollectionChanged
    {
        [NonSerialized] protected readonly LiveData<TDataType> ItemsLiveData = new LiveData<TDataType>();
        public IReadOnlyList<TDataType> ItemsData => ItemsLiveData.GetData;

            public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => ItemsLiveData.CollectionChanged += value;
            remove => ItemsLiveData.CollectionChanged -= value;
        }
    }
}