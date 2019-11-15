using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class MarketplaceViewModel : ViewsSwitchingViewModel
    {
        [Binding]
        public void SwitchToViewGamesView()
        {
            SwitchToView(nameof(GamesView));
        }
    }
}