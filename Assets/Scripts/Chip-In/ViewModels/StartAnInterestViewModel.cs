using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public sealed class StartAnInterestViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private bool _poolAndFund;
        private bool _isPublic;

        private string _posterImagePath;
        private string _messageForMembers;
        private string _messageFormMerchants;

        private string _interestName;


        [Binding]
        public string InterestName
        {
            get => _interestName;
            set
            {
                if (value == _interestName) return;
                _interestName = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int PoolAndFund
        {
            set
            {
                switch (value)
                {
                    case 0:
                    {
                        _poolAndFund = true;
                        break;
                    }
                    case 1:
                    {
                        _poolAndFund = false;
                        break;
                    }
                }

                OnPropertyChanged();
            }
        }

        [Binding]
        public int IsPublic
        {
            set
            {
                switch (value)
                {
                    case 0:
                    {
                        _isPublic = false;
                        break;
                    }
                    case 1:
                    {
                        _isPublic = true;
                        break;
                    }
                }

                OnPropertyChanged();
            }
        }


        [Binding]
        public string MessageFormMerchants
        {
            get => _messageFormMerchants;
            set
            {
                if (value == _messageFormMerchants) return;
                _messageFormMerchants = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string MessageForMembers
        {
            get => _messageForMembers;
            set
            {
                if (value == _messageForMembers) return;
                _messageForMembers = value;
                OnPropertyChanged();
            }
        }


        [Binding]
        public string PosterImagePath
        {
            get => _posterImagePath;
            set
            {
                if (value == _posterImagePath) return;
                _posterImagePath = value;
                OnPropertyChanged();
            }
        }

        public StartAnInterestViewModel() : base(nameof(StartAnInterestViewModel))
        {
        }

        [Binding]
        public void CreateButton_OnClick()
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