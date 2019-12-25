using UI.Elements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Views
{
    [DisallowMultipleComponent]
    public abstract class BaseView: UIBehaviour
    {
        private RectTransform _viewRootRectTransform;
        public string GetViewName => GetType().Name;
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
    }
}