using System.Collections.Generic;

namespace Common
{
    public class History
    {
        private string _currentRecord;
        private readonly Stack<string> _historyStack;

        public History()
        {
            _historyStack = new Stack<string>();
        }

        public void AddToHistory(in string record)
        {
            if (!string.IsNullOrEmpty(_currentRecord))
                _historyStack.Push(_currentRecord);
            _currentRecord = record;
        }

        public string PopHistoryStack()
        {
            _currentRecord = _historyStack.Pop();
            return _currentRecord;
        }
    }
}