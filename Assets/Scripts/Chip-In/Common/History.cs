using System.Collections.Generic;

namespace Common
{
    public class History<T>
    {
        private bool _isFirstRecord = true;
        private T _currentRecord;
        private readonly Stack<T> _historyStack;

        public History()
        {
            _historyStack = new Stack<T>();
        }

        public void AddToHistory(in T record)
        {
            if (_isFirstRecord)
            {
                _isFirstRecord = false;
                _currentRecord = record;
                return;
            }

            _historyStack.Push(_currentRecord);
            _currentRecord = record;
        }

        public void ClearHistory()
        {
            _historyStack.Clear();
        }

        public T PopHistoryStack()
        {
            _currentRecord = _historyStack.Pop();
            return _currentRecord;
        }
    }
}