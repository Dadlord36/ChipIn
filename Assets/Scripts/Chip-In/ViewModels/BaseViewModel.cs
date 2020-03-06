using UnityEngine;
using UnityEngine.Assertions;
using Views;

namespace ViewModels
{
    /// <summary>
    /// Base view model class
    /// </summary>
    public abstract class BaseViewModel : MonoBehaviour, INotifySwitching
    {
        [SerializeField] private BaseView view;

        public BaseView View => view;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!Application.isPlaying)
                TryGetComponent(out view);
            Assert.IsNotNull(view);
        }
#endif
        protected virtual void OnEnable()
        {
            View.SetViewModelSwitchNotifier(this);
        }

        protected virtual void OnDisable()
        {
        }

        /// <summary>
        /// Fires up when view becoming the one, that user is currently interacts with
        /// </summary>
        protected virtual void OnBecomingActiveView()
        {
        }

        /// <summary>
        /// Fires up when view becoming inactive, so that user can't interact with it anymore
        /// </summary>
        protected virtual void OnBecomingInactiveView()
        {
        }

        void INotifySwitching.BeingSwitchedTo()
        {
            OnBecomingActiveView();
        }

        void INotifySwitching.BeingSwitchedFrom()
        {
            OnBecomingInactiveView();
        }
    }
}