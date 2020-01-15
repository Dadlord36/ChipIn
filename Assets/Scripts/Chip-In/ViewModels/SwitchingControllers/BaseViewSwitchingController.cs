using Common;
using UnityEngine;
using UnityEngine.Assertions;
using ViewModels.Interfaces;

namespace ViewModels.SwitchingControllers
{
    public abstract class BaseViewSwitchingController : ScriptableObject, IViewsSwitchingController
    {
        [SerializeField] protected Object viewsSwitchingBindingObject;
        [SerializeField] private bool shouldRecordHistory = true;
        private History<string> _viewsSwitchingNamesHistory;

        protected virtual void OnEnable()
        {
            Assert.IsNotNull(viewsSwitchingBindingObject);
            InitializeViewSwitchingHistory();
        }

        public void SwitchToPreviousView()
        {
            ProcessViewsSwitching(_viewsSwitchingNamesHistory.PopHistoryStack());
        }

        private void AddViewsSwitchingHistoryRecord(in string viewName)
        {
            _viewsSwitchingNamesHistory.AddToHistory(viewName);
        }

        private void InitializeViewSwitchingHistory()
        {
            if (!shouldRecordHistory) return;

            if (_viewsSwitchingNamesHistory == null)
                _viewsSwitchingNamesHistory = new History<string>();
            else
            {
                ClearHistory();
            }
        }

        private void ClearHistory()
        {
            _viewsSwitchingNamesHistory.ClearHistory();
        }

        public void RequestSwitchToView(in string viewName)
        {
            if (shouldRecordHistory)
                AddViewsSwitchingHistoryRecord(viewName);

            ProcessViewsSwitching(viewName);
        }

        protected abstract void ProcessViewsSwitching(in string viewNameToSwitchTo);
    }
}