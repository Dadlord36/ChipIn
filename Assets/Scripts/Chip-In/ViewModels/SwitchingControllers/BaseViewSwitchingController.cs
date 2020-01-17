using Common;
using UnityEngine;
using UnityEngine.Assertions;
using ViewModels.Interfaces;

namespace ViewModels.SwitchingControllers
{
    public abstract class BaseViewSwitchingController : ScriptableObject, IViewsSwitchingController
    {
        [SerializeField] protected Object viewsSwitchingBindingObject;
        [SerializeField] private SwitchingHistoryController switchingHistoryController;

        protected virtual void OnEnable()
        {
            Assert.IsNotNull(viewsSwitchingBindingObject);
            switchingHistoryController.InitializeViewSwitchingHistory();
        }

        private void AddToHistoryStack(string viewName)
        {
            switchingHistoryController.AddViewsSwitchingHistoryRecord(viewName);
        }

        public void SwitchToPreviousView()
        {
            RequestSwitchToView(switchingHistoryController.PopHistoryStack());
        }

        public void RequestSwitchToView(string toViewName)
        {
            AddToHistoryStack(toViewName);
            ProcessViewsSwitching(toViewName);
        }

        protected abstract void ProcessViewsSwitching(in string viewNameToSwitchTo);
    }
}