using System.Collections.Generic;
using System.Collections.Specialized;
using Common;
using UnityEngine;

namespace Repositories.Remote
{
    public class BaseItemsListRepository<TDataType> : ScriptableObject, INotifyCollectionChanged
    {
        private readonly LiveData<TDataType> _data = new LiveData<TDataType>();
        public IReadOnlyList<TDataType> Data => _data.GetData;


        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => _data.CollectionChanged += value;
            remove => _data.CollectionChanged -= value;
        }
    }
}