using Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using ViewModels.Interfaces;
using Views;

namespace ViewModels.Helpers
{
    public abstract class BaseViewSwitchingHelper : MonoBehaviour, IViewsSwitchingHelper
    {
        [SerializeField] protected Object viewsSwitchingBindingObject;
        private static IViewsSwitchingHelper _instance;
        public static IViewsSwitchingHelper Instance => _instance;

        private History<string> _viewsSwitchingNamesHistory;

        protected virtual void Awake()
        {
            _instance = this;
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
            if (_viewsSwitchingNamesHistory == null)
                _viewsSwitchingNamesHistory = new History<string>();
        }

        public void RequestSwitchToView(in string viewName)
        {
            AddViewsSwitchingHistoryRecord(viewName);
            ProcessViewsSwitching(viewName);
        }

        protected abstract void ProcessViewsSwitching(in string viewNameToSwitchTo);
    }
}