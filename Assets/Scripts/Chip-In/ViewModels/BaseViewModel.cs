using UnityEngine;
using UnityEngine.Assertions;
using Views;

namespace ViewModels
{
    /// <summary>
    /// Base view model class
    /// </summary>
    public abstract class BaseViewModel : MonoBehaviour
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
        }

        protected virtual void OnDisable()
        {
        }
    }
}