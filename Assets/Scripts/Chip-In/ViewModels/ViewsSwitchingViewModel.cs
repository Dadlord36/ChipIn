using Factories;
using UnityEngine;
using UnityEngine.Assertions;
using ViewModels.Interfaces;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel
    {
        [SerializeField] private GameObject viewsSwitchingHelperObject;
        private IViewsSwitchingHelper _viewsSwitchingHelper;

        protected virtual void Start()
        {
            InitializeViewsSwitchingHelper();
        }

        private void InitializeViewsSwitchingHelper()
        {
            if (viewsSwitchingHelperObject == null)
                _viewsSwitchingHelper = Factory.CreateMultiViewSwitchingHelper();
            else
            {
                _viewsSwitchingHelper = viewsSwitchingHelperObject.GetComponent<IViewsSwitchingHelper>();
            }
            Assert.IsNotNull(_viewsSwitchingHelper);
        }

        protected void SwitchToView(in string viewName)
        {
            _viewsSwitchingHelper.RequestSwitchToView(viewName);
        }

        public void SwitchToPreviousView()
        {
            _viewsSwitchingHelper.SwitchToPreviousView();
        }
    }
}