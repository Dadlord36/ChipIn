using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels.Interfaces;
using DataModels.SimpleTypes;
using JetBrains.Annotations;

namespace DataModels
{
    public sealed class FlashOfferDataModel : IFlashOfferModel, INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private int _quantity;
        private int _tokensAmount;
        private FilePath _posterFilePath;

        public string Title
        {
            get => _title;
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value == _quantity) return;
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        public FilePath PosterFilePath
        {
            get => _posterFilePath;
            set
            {
                if (Equals(value, _posterFilePath)) return;
                _posterFilePath = value;
                OnPropertyChanged();
            }
        }

        public int TokensAmount
        {
            get => _tokensAmount;
            set
            {
                if (value == _tokensAmount) return;
                _tokensAmount = value;
                OnPropertyChanged();
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    
    }
}