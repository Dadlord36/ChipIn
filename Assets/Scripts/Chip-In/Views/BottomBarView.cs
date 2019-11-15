namespace Views
{
    public class BottomBarView : BaseView
    {

        public void SwitchSelectedButton(string currentView)
        {
            switch (currentView)
            {
                case nameof(MarketplaceView):
                    Show();
                    return;
                case nameof(ChallengesView) :
                    Show();
                    return;
                case nameof(CartView):
                    Show();
                    return;
                case nameof(CommunityView):
                    Show();
                    return;
                case nameof(SettingsView):
                    Show();
                    return;
            }
            Hide();
        }
    }
}