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
        [SerializeField] private BottomBarView bottomBarView;

        private IViewsSwitchingBinding _viewsSwitchingBinding;
//        private IActivityConnector _bottomBarActivityConnector;

        private BaseView _currentView;
        private string _previousViewName, _currentViewName;


        private void Awake()
        {
            _instance = this;
            Assert.IsNotNull(viewsSwitchingBindingObject);
            _viewsSwitchingBinding = viewsSwitchingBindingObject as IViewsSwitchingBinding;
            Assert.IsNotNull(_viewsSwitchingBinding);
        }


        public void SwitchToView(string viewName)
        {
            _previousViewName = _currentViewName;
            _currentViewName = viewName;

            _viewsSwitchingBinding.SwitchViews(_currentView, viewName);
            bottomBarView.SwitchSelectedButton(viewName);
        }

        public void SwitchToPreviousView()
        {
            SwitchToView(_previousViewName);
        }
    }
}