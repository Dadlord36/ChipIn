using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Views
{
    [DisallowMultipleComponent]
    public abstract class BaseView : UIBehaviour
    {
        public event Action BeingSwitchedTo;
        public event Action BeingSwitchedSwitchedFrom;

        private RectTransform _viewRootRectTransform;
        public string ViewName => GetType().Name;
        public RectTransform ViewRootRectTransform => _viewRootRectTransform;

        protected override void Awake()
        {
            Assert.IsTrue(TryGetComponent(out _viewRootRectTransform), "There is no RectTransform on GameObject," +
                                                                       "where ViewModel is attached");
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ConfirmBeingSwitchedTo()
        {
            OnBeingSwitchedTo();
        }

        public void ConfirmedBeingSwitchedFrom()
        {
            OnBeingSwitchedSwitchedFrom();
        }

        private void OnBeingSwitchedTo()
        {
            BeingSwitchedTo?.Invoke();
        }

        private void OnBeingSwitchedSwitchedFrom()
        {
            BeingSwitchedSwitchedFrom?.Invoke();
        }
    }
}