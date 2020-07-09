using System.Collections.Generic;

namespace Common
{
    public class LimitedStack<T> : LinkedList<T>
    {
        private readonly int _maxSize;

        public LimitedStack(int maxSize)
        {
            _maxSize = maxSize;
        }

        public void Push(T item)
        {
            AddFirst(item);

            if (Count > _maxSize)
                RemoveLast();
        }

        public T Pop()
        {
            var item = First.Value;
            RemoveFirst();
            return item;
        }
    }
}