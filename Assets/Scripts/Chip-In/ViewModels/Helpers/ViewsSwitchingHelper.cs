using ScriptableObjects.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using Views;

namespace ViewModels.Helpers
{
    public class ViewsSwitchingHelper : MonoBehaviour, IViewsSwitchingHelper
    {
        private static IViewsSwitchingHelper _instance;
        public static IViewsSwitchingHelper Instance => _instance;
        
        [SerializeField] private Object viewsSwitchingBindingObject;

        private IViewsSwitchingBinding _viewsSwitchingBinding;
        private BaseView _currentView;

        private void Awake()
        {
            _instance = this;
            Assert.IsNotNull(viewsSwitchingBindingObject);
            _viewsSwitchingBinding = viewsSwitchingBindingObject as IViewsSwitchingBinding;
            Assert.IsNotNull(_viewsSwitchingBinding);
        }

        public void SwitchToView(in string viewName)
        {
            _viewsSwitchingBinding.SwitchViews(_currentView, viewName);
        }
    }
}