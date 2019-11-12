using UI.Elements;
using UnityEngine;

namespace Views
{
    public class BottomBarView : BaseView
    {
        [SerializeField] private BarButtonSelection marketplaceSelectionView,
            challengesSelectionView,
            cartSelectionView,
            communitySelectionView,
            settingsSelectionView;

        public void ChangeViewActivityBasedOnCurrentViewName(string currentView)
        {
            switch (currentView)
            {
                case nameof(MarketplaceView):
                    Show();
                    marketplaceSelectionView.SelectAsOneOfGroup();
                    return;
                case nameof(ChallengesView) :
                    Show();
                    challengesSelectionView.SelectAsOneOfGroup();
                    return;
                case nameof(CartView):
                    Show();
                    cartSelectionView.SelectAsOneOfGroup();
                    return;
                case nameof(CommunityView):
                    Show();
                    communitySelectionView.SelectAsOneOfGroup();
                    return;
                case nameof(SettingsView):
                    Show();
                    settingsSelectionView.SelectAsOneOfGroup();
                    return;
            }
            Hide();
        }
    }
}