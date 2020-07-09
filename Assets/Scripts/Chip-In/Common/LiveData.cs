using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Common
{
    public class LiveData<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        protected readonly List<T> Items;

        public LiveData(int length)
        {
            Items = new List<T>(length);
        }

        public LiveData(T[] items)
        {
            Items = new List<T>(items);
        }

        public IReadOnlyList<T> DataList => Items;

        public LiveData()
        {
            Items = new List<T>();
        }

        protected LiveData(IEnumerable<T> itemsList)
        {
            Items = new List<T>(itemsList);
        }

        public void Add(T item)
        {
            Items.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void Clear()
        {
            Items.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void AddRange(T[] itemsArray)
        {
            Items.AddRange(itemsArray);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itemsArray));
        }

        public void Remove(T item)
        {
            Items.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }


        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Items).GetEnumerator();
        }

        public T this[int index] // Indexer declaration  
        {
            get => Items[index];
            set => Items[index] = value;
        }
    }
}