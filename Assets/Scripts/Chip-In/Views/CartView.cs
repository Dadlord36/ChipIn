using Views.Base;

namespace Views
{
    public sealed class CartView : ItemsListBaseView
    {
        public CartView() : base(nameof(CartView))
        {
        }
    }
}