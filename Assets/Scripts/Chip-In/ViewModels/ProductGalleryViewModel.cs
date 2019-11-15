using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class ProductGalleryViewModel : ViewsSwitchingViewModel
    {
        [Binding]
        public void SwitchToChallengesView()
        {
            SwitchToView(nameof(ChallengesView));
        }
    }
}