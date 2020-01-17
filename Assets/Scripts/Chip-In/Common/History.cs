using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class History<T>
    {
        private readonly Stack<T> _historyStack;
        private readonly LimitedStack<T> _dualRecords;

        private const string Tag = "History"; 
        
        public History()
        {
            _dualRecords = new LimitedStack<T>(2);
            _historyStack = new Stack<T>();
        }

        public void AddToHistory(in T record)
        {
            if (_dualRecords.Count > 0)
            {
                var previousRecord = _dualRecords.Pop();
                _historyStack.Push(previousRecord);
                Debug.unityLogger.Log(LogType.Log,Tag, $"History record: {previousRecord.ToString()}");
            }

            _dualRecords.Push(record);
        }

        public void ClearHistory()
        {
            _dualRecords.Clear();
            _historyStack.Clear();
        }

        public T PopHistoryStack()
        {
            return _historyStack.Pop();
        }
    }
}