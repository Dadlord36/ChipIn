using Common;
using ScriptableObjects.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using ViewModels.Interfaces;
using Views;

namespace ViewModels.Helpers
{
    public class MultiViewsSwitchingHelper : MonoBehaviour, IViewsSwitchingHelper
    {
        [SerializeField] private Object viewsSwitchingBindingObject;
        
        private static IViewsSwitchingHelper _instance;
        public static IViewsSwitchingHelper Instance => _instance;
        private IMultiViewsSwitchingBinding _multiViewsSwitchingBinding;
        
        private BaseView _currentView;
        private History<string> _viewsSwitchingNamesHistory;

        private void Awake()
        {
            _instance = this;
            Assert.IsNotNull(viewsSwitchingBindingObject);
            _multiViewsSwitchingBinding = viewsSwitchingBindingObject as IMultiViewsSwitchingBinding;
            Assert.IsNotNull(_multiViewsSwitchingBinding);
            InitializeViewSwitchingHistory();
        }

        private void InitializeViewSwitchingHistory()
        {
            if (_viewsSwitchingNamesHistory == null)
                _viewsSwitchingNamesHistory = new History<string>();
        }

        public void RequestSwitchToView(string viewName)
        {
            AddViewsSwitchingHistoryRecord(viewName);
            ProcessViewsSwitching(viewName);
        }

        private void ProcessViewsSwitching(in string viewNameToSwitchTo)
        {
            _multiViewsSwitchingBinding.SwitchViews(_currentView, viewNameToSwitchTo);
        }

        private void AddViewsSwitchingHistoryRecord(in string viewName)
        {
            _viewsSwitchingNamesHistory.AddToHistory(viewName);
        }

        public void SwitchToPreviousView()
        {
            ProcessViewsSwitching(_viewsSwitchingNamesHistory.PopHistoryStack());
        }
    }
}