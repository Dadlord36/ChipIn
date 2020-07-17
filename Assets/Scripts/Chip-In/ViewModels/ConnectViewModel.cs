using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public sealed class ConnectViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private bool _listIsNotEmpty;

        [Binding]
        public bool ListIsNotEmpty
        {
            get => _listIsNotEmpty;
            private set
            {
                if (value == _listIsNotEmpty) return;
                _listIsNotEmpty = value;
                OnPropertyChanged();
            }
        }

        public ConnectViewModel() : base(nameof(ConnectViewModel))
        {
        }

        [Binding]
        public void CreateAdButton_OnClick()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}