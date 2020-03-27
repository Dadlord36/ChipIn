using ActionsTranslators;
using InputDetection;
using ScriptableObjects.Comparators;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
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
        [SerializeField] private ViewsComparisonContainer associativeViewsContainer;
        [SerializeField] private MainInputActionsTranslator inputActionsTranslator;

        private string _currentViewName;

        private void OnSwiped(SwipeDetector.SwipeData swipeData)
        {
            switch (swipeData.Direction)
            {
                case MoveDirection.Left:
                    SwitchToLefterBarItem();
                    break;
                case MoveDirection.Right:
                    SwitchToRighterBarItem();
                    break;
            }
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
            inputActionsTranslator.Swiped += OnSwiped;
        }

        public void OnHideOut()
        {
            inputActionsTranslator.Swiped -= OnSwiped;
        }


        /*[Binding]
        public void SwitchToMarketplaceView()
        {
            SwitchToViewAndChooseAppearingSide(nameof(MarketplaceView));
        }

        [Binding]
        public void SwitchToChallengesView()
        {
            SwitchToViewAndChooseAppearingSide(nameof(MyChallengeView));
        }

        [Binding]
        public void SwitchToCartView()
        {
            SwitchToViewAndChooseAppearingSide(nameof(CartView));
        }

        [Binding]
        public void SwitchToCommunityView()
        {
            SwitchToViewAndChooseAppearingSide(nameof(CommunityView));
        }

        [Binding]
        public void SwitchToSettingsView()
        {
            SwitchToViewAndChooseAppearingSide(nameof(SettingsView));
        }*/

        void INotifyViewSwitching.OnViewSwitched(string viewName)
        {
            _currentViewName = viewName;
        }

        /*private void SwitchToSelectedView(string viewName)
        {
            SwitchToViewAndChooseAppearingSide(viewName);
        }*/

        private void SwitchToLefterBarItem()
        {
            TrySwitchToRelativeView(ViewsComparisonContainer.RelativePositionInArray.After);
        }

        private void SwitchToRighterBarItem()
        {
            TrySwitchToRelativeView(ViewsComparisonContainer.RelativePositionInArray.Before);
        }

        private void TrySwitchToRelativeView(ViewsComparisonContainer.RelativePositionInArray relativePositionInArray)
        {
            if (associativeViewsContainer.GetRelativeViewName(_currentViewName, relativePositionInArray, out var viewName))
            {
                SwitchToViewAndChooseAppearingSide(viewName);
            }
        }

        private void SwitchToViewAndChooseAppearingSide(string viewToSwitchToName)
        {
            var relativePositionInArray = associativeViewsContainer.GetRelativePositionInContainer(_currentViewName, viewToSwitchToName);

            SwitchToView(viewToSwitchToName, _currentViewName,
                relativePositionInArray == ViewsComparisonContainer.RelativePositionInArray.Before
                    ? ViewsSwitchData.AppearingSide.FromLeft
                    : ViewsSwitchData.AppearingSide.FromRight);
        }
    }
}