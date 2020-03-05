using UnityEngine;
using UnityEngine.EventSystems;

namespace Views
{
    public interface INotifySwitching
    {
        void BeingSwitchedTo();
        void BeingSwitchedFrom();
    }
    
    [DisallowMultipleComponent]
    public abstract class BaseView : UIBehaviour
    {
        public string ViewName => GetType().Name;
        public RectTransform ViewRootRectTransform => transform as RectTransform;
        private INotifySwitching _viewModelNotifier;

        public void SetViewModelNotifier(INotifySwitching viewModelNotifier)
        {
            _viewModelNotifier = viewModelNotifier;
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
            _viewModelNotifier.BeingSwitchedTo();
        }

        private void OnBeingSwitchedSwitchedFrom()
        {
            _viewModelNotifier.BeingSwitchedFrom();
        }
    }
}