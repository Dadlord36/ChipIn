using ScriptableObjects.SwitchBindings;
using UnityEngine.Assertions;
using UnityWeld.Binding;
using ViewModels.UI.Elements.Buttons;
using Views;
using Views.Bars;

public interface INotifyViewSwitching
{
    void OnViewSwitched(string viewName);
}

namespace ViewModels
{
    [Binding]
    public class BottomBarViewModel : ViewsSwitchingViewModel, INotifyVisibilityChanged, INotifyViewSwitching
    {
        private readonly ViewsSwitchingParameters _defaultViewsSwitchingParameters = new ViewsSwitchingParameters(
            ViewAppearanceParameters.Idle(ViewAppearanceParameters.SwitchingViewPosition.Under),
            ViewAppearanceParameters.JustFading(ViewAppearanceParameters.SwitchingViewPosition.Above)
        );

        private string _currentViewName;

        public BottomBarViewModel() : base(nameof(BottomBarViewModel))
        {
        }

        private void SubscribeBarButtons()
        {
            foreach (var barButton in GetComponentsInChildren<BarButtonSelection>())
            {
                barButton.ViewSelected += SwitchToViewAndChooseAppearingSide;
            }
        }

        private void UnsubscribeBarButtons()
        {
            foreach (var barButton in GetComponentsInChildren<BarButtonSelection>())
            {
                barButton.ViewSelected -= SwitchToViewAndChooseAppearingSide;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            View.SetViewModelVisibilityNotifier(this);
            ((BottomBarView) View).SetViewsSwitchingListener(this);
            SubscribeBarButtons();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeBarButtons();
        }

        private void Awake()
        {
            Assert.IsNotNull(View as BottomBarView);
        }

        public void OnShowUp()
        {
        }

        public void OnHideOut()
        {
        }


        void INotifyViewSwitching.OnViewSwitched(string viewName)
        {
            _currentViewName = viewName;
        }

        private void SwitchToViewAndChooseAppearingSide(string viewToSwitchToName)
        {
            SwitchToView(new ViewsPairInfo(_currentViewName, viewToSwitchToName), _defaultViewsSwitchingParameters, false);
        }
    }
}