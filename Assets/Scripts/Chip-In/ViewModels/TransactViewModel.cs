using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class TransactViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private bool _alternativeDiagram;

        [Binding]
        public bool AlternativeDiagram
        {
            get => _alternativeDiagram;
            set
            {
                if (value == _alternativeDiagram) return;
                _alternativeDiagram = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public void Redemption_OnClick()
        {
            SwitchToQrCodeForm();
        }

        private void SwitchToQrCodeForm()
        {
            SwitchToView(nameof(QrCodeScannerView));
        }

        [Binding]
        public void SwitchDiagram()
        {
            AlternativeDiagram = !AlternativeDiagram;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}