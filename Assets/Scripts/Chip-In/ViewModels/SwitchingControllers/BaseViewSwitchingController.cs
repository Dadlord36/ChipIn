using Common;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.Assertions;
using ViewModels.Interfaces;

namespace ViewModels.SwitchingControllers
{
    public abstract class BaseViewSwitchingController : ScriptableObject, IViewsSwitchingController
    {
        [SerializeField] protected ViewsSwitchingBinding viewsSwitchingBindingObject;
        [SerializeField] private SwitchingHistoryController switchingHistoryController;

        [SerializeField]
        private ViewsSwitchData.AppearingSide defaultReturningViewAppearingSide = ViewsSwitchData.AppearingSide.FromLeft;

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
            ProcessViewsSwitching(switchingHistoryController.PopHistoryStack(), defaultReturningViewAppearingSide);
        }

        public void RequestSwitchToView(string fromViewName, string toViewName,
            ViewsSwitchData.AppearingSide viewAppearingSide= ViewsSwitchData.AppearingSide.FromRight)
        {
            if (!string.IsNullOrEmpty(fromViewName))
                AddToHistoryStack(fromViewName);
            ProcessViewsSwitching(toViewName,viewAppearingSide);
        }

        protected abstract void ProcessViewsSwitching(in string viewNameToSwitchTo,
            ViewsSwitchData.AppearingSide viewAppearingSide);
    }
}