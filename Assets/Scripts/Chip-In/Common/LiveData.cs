using System.Collections.Generic;
using System.Collections.Specialized;

namespace Common
{
    public sealed class LiveData<T> : INotifyCollectionChanged
    {

        private List<T> _items;
        
        public LiveData (int length)
        {
            _items = new List<T>(length);   
        }

        public IReadOnlyList<T> GetData => _items;
        
        public LiveData()
        {
            _items = new List<T>();
        }

        public void Add(T item)
        {
            _items.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void Remove(T item)
        {
            _items.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }


        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
    }
}