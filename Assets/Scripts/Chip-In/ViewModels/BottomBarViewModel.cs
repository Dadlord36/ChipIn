using ScriptableObjects.Comparators;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.Assertions;
using UnityWeld.Binding;
using Views;
using Views.Bars;

namespace ViewModels
{
    [Binding]
    public class BottomBarViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private ViewsComparisonContainer associativeViewsContainer;

        private void Awake()
        {
            Assert.IsNotNull(View as BottomBarView);
        }

        private string CurrentView => ((BottomBarView) View).CurrentViewName;

        [Binding]
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
        }


        private void SwitchToViewAndChooseAppearingSide(string viewToSwitchToName)
        {
            var relativePositionInArray =
                associativeViewsContainer.GetRelativePositionInContainer(CurrentView, viewToSwitchToName);

            SwitchToView(viewToSwitchToName, CurrentView,
                relativePositionInArray == ViewsComparisonContainer.RelativePositionInArray.Before
                    ? ViewsSwitchData.AppearingSide.FromLeft
                    : ViewsSwitchData.AppearingSide.FromRight);
        }
    }
}