using System;
using ScriptableObjects.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using Views;
using Object = UnityEngine.Object;

namespace ViewModels.Helpers
{
    public class ViewsSwitchingHelper : MonoBehaviour, IViewsSwitchingHelper
    {
        private static IViewsSwitchingHelper _instance;

        #region Events

        public event Action<string> SwitchedToView;

        #endregion

        public static IViewsSwitchingHelper Instance => _instance;

        [SerializeField] private Object viewsSwitchingBindingObject;


        private IViewsSwitchingBinding _viewsSwitchingBinding;
        private IActivityConnector _bottomBarActivityConnector;

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
//            SwitchedToView?.Invoke();
        }
    }
}