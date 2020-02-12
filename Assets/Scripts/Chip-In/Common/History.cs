using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class History<T>
    {
        private readonly Stack<T> _historyStack;
        // private readonly LimitedStack<T> _dualRecords;

        private const string Tag = "History"; 
        
        public History()
        {
            // _dualRecords = new LimitedStack<T>(2);
            _historyStack = new Stack<T>();
        }

        public void AddToHistory(in T record)
        {
            /*if (_dualRecords.Count > 0)
            {
                // var previousRecord = _dualRecords.Pop();

            }

            _dualRecords.Push(record);*/
            
            _historyStack.Push(record);
            Debug.unityLogger.Log(LogType.Log,Tag, $"History record: {record.ToString()}");
        }

        public void ClearHistory()
        {
            // _dualRecords.Clear();
            _historyStack.Clear();
        }

        public T PopHistoryStack()
        {
            return _historyStack.Pop();
        }
    }
}