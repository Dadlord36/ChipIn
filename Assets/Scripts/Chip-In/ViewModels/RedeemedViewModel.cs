using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class RedeemedViewModel : ViewsSwitchingViewModel
    {
        public RedeemedViewModel() : base(nameof(RedeemedViewModel))
        {
        }

        [Binding]
        public void CancelButton_OnClick()
        {
            SwitchToView(nameof(TransactView));
        }
    }
}