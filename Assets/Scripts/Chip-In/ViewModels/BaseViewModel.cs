using UnityEngine;
using UnityEngine.Assertions;

namespace ViewModels
{
    /// <summary>
    /// Base view model class
    /// </summary>
    public abstract class BaseViewModel : MonoBehaviour
    {
        private RectTransform _viewRootRectTransform;

        public RectTransform ViewRootRectTransform => _viewRootRectTransform;

        private void Awake()
        {
            Assert.IsTrue(TryGetComponent(out _viewRootRectTransform), "There is no RectTransform on GameObject," +
                                                                       "where ViewModel is attached");
        }
    }
}