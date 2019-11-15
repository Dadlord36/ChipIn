using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class ProductMenuViewModel : ViewsSwitchingViewModel
    {
        [Binding]
        public void SwitchToProductGallery()
        {
            SwitchToView(nameof(ProductMenuView));
        }
    }
}