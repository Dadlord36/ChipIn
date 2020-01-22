using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Common;
using DataModels.Common;
using RequestsStaticProcessors;

namespace Repositories.Remote
{
    public abstract class BaseItemsListRepository<TDataType> : RemoteRepositoryBase, INotifyCollectionChanged
    {
        [NonSerialized] protected PaginatedList<TDataType> ItemsLiveData = new PaginatedList<TDataType>();
        public IReadOnlyList<TDataType> ItemsData => ItemsLiveData.GetData;
        public PaginationData Pagination => ItemsLiveData.Pagination;


        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => ItemsLiveData.CollectionChanged += value;
            remove => ItemsLiveData.CollectionChanged -= value;
        }
    }
}