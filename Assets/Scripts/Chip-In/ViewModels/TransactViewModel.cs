using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class TransactViewModel : ViewsSwitchingViewModel
    {
        [Binding]
        public void Redemption_OnClick()
        {
            SwitchToQrCodeForm();
        }

        private void SwitchToQrCodeForm()
        {
            SwitchToView(nameof(QrCodeScannerView));
        }
    }
}