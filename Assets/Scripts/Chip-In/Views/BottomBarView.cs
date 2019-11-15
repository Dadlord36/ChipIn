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
                case nameof(MyChallengeView):
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