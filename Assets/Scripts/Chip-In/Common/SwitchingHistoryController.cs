using System;
using UnityEngine;

namespace Common
{
    [Serializable]
    public sealed class SwitchingHistoryController
    {
        [SerializeField] private bool shouldRecordHistory = true;
        private static History<string> _viewsSwitchingNamesHistory;

        public void InitializeViewSwitchingHistory()
        {
            if (!shouldRecordHistory) return;

            if (_viewsSwitchingNamesHistory == null)
                _viewsSwitchingNamesHistory = new History<string>();
            else
            {
                ClearHistory();
            }
        }
        
        public string PopHistoryStack()
        {
            return _viewsSwitchingNamesHistory.PopHistoryStack();
        }
        
        public void AddViewsSwitchingHistoryRecord(in string viewName)
        {
            if (!shouldRecordHistory) return;
            
            _viewsSwitchingNamesHistory.AddToHistory(viewName);
        }
        
        public void ClearHistory()
        {
            _viewsSwitchingNamesHistory.ClearHistory();
        }
        
    }
}