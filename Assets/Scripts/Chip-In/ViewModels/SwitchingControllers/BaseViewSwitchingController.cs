﻿using Common;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.Assertions;

namespace ViewModels.SwitchingControllers
{
    public abstract class BaseViewSwitchingController : ScriptableObject
    {
        [SerializeField] protected ViewsSwitchingBinding viewsSwitchingBindingObject;
        [SerializeField] private SwitchingHistoryController switchingHistoryController;

        protected virtual void OnEnable()
        {
            Assert.IsNotNull(viewsSwitchingBindingObject);
            switchingHistoryController.InitializeViewSwitchingHistory();
        }

        public void RemoveExistingViewInstance(in string viewName)
        {
            viewsSwitchingBindingObject.RemoveExistingViewInstance(viewName);
        }

        private void AddToHistoryStack(string viewName)
        {
            switchingHistoryController.AddViewsSwitchingHistoryRecord(viewName);
        }

        public void ClearSwitchingHistory()
        {
            switchingHistoryController.ClearHistory();
        }

        public void SwitchToPreviousView()
        {
            ProcessViewsSwitching(null, switchingHistoryController.PopHistoryStack());
        }

        public void RequestSwitchToView(string fromViewName, string toViewName, FormsTransitionBundle formsTransitionBundle = default)
        {
            if (!string.IsNullOrEmpty(fromViewName))
                AddToHistoryStack(fromViewName);
            ProcessViewsSwitching(fromViewName, toViewName, formsTransitionBundle);
        }

        protected abstract void ProcessViewsSwitching(in string fromViewName, in string toViewName,
            FormsTransitionBundle formsTransitionBundle = default);
    }
}