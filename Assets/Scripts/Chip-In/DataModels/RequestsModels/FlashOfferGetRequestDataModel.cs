using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels.Interfaces;
using DataModels.SimpleTypes;
using JetBrains.Annotations;

namespace DataModels.RequestsModels
{
    public sealed class FlashOfferGetRequestDataModel : IFlashOfferGetRequestModel, INotifyPropertyChanged
    {
        private FilePath _posterFilePath;
        private string _category;
        private string _title;
        private string _description;
        private uint _quantity;
        private string _radius;
        private uint _tokensAmount = 1;
        private DateTime _expireDate;

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

        public string Category
        {
            get => _category;
            set
            {
                if (value == _category) return;
                _category = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (value == _title) return;
                _title = value;
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
            }
        }

        public uint Quantity
        {
            get => _quantity;
            set
            {
                if (value == _quantity) return;
                _quantity = value;
                OnPropertyChanged();
            }
        }

        public string Radius
        {
            get => _radius;
            set
            {
                if (value == _radius) return;
                _radius = value;
                OnPropertyChanged();
            }
        }

        public uint TokensAmount
        {
            get => _tokensAmount;
            set
            {
                if (value == _tokensAmount) return;
                _tokensAmount = value;
                OnPropertyChanged();
            }
        }

        public DateTime ExpireDate
        {
            get => _expireDate;
            set
            {
                if (value.Equals(_expireDate)) return;
                _expireDate = value;
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