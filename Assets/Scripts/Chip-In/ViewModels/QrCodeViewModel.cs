using Views;

namespace ViewModels
{
    public class QrCodeViewModel : ViewsSwitchingViewModel
    {
        private QrCodeView ThisView => View as QrCodeView;

        private void SetQrCodeInView(string qrCode)
        {
            ThisView.QrCode = qrCode;
        }

        public QrCodeViewModel() : base(nameof(QrCodeViewModel))
        {
        }
    }
}