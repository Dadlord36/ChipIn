﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Views
{
    public interface INotifySwitching
    {
        void BeingSwitchedTo();
        void BeingSwitchedFrom();
    }

    public interface INotifyVisibilityChanged
    {
        void OnShowUp();
        void OnHideOut();
    }

    [DisallowMultipleComponent]
    public abstract class BaseView : UIBehaviour
    {
        public string ViewName => GetType().Name;
        public RectTransform ViewRootRectTransform => transform as RectTransform;
        private INotifySwitching _viewModelNotifier;
        private INotifyVisibilityChanged _viewModelVisibilityNotifier;

        public void SetViewModelSwitchNotifier(INotifySwitching switchingNotifier)
        {
            _viewModelNotifier = switchingNotifier;
        }

        public void SetViewModelVisibilityNotifier(INotifyVisibilityChanged visibilityChangedNotifier)
        {
            _viewModelVisibilityNotifier = visibilityChangedNotifier;
        }

        public void Show()
        {
            if (gameObject.activeSelf) return;

            gameObject.SetActive(true);
            OnBeingShowUp();
        }

        public void Hide()
        {
            if (!gameObject.activeSelf) return;

            gameObject.SetActive(false);
            OnBeingHide();
        }

        public void ConfirmBeingSwitchedTo()
        {
            OnBeingSwitchedTo();
        }

        public void ConfirmedBeingSwitchedFrom()
        {
            OnBeingSwitchedSwitchedFrom();
        }

        private void OnBeingShowUp()
        {
            _viewModelVisibilityNotifier?.OnShowUp();
        }

        private void OnBeingHide()
        {
            _viewModelVisibilityNotifier?.OnHideOut();
        }

        private void OnBeingSwitchedTo()
        {
            _viewModelNotifier?.BeingSwitchedTo();
        }

        private void OnBeingSwitchedSwitchedFrom()
        {
            _viewModelNotifier?.BeingSwitchedFrom();
        }
    }
}