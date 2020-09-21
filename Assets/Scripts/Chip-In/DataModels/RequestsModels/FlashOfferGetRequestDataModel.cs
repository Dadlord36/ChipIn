using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels.Interfaces;
using JetBrains.Annotations;

namespace DataModels.RequestsModels
{
    public sealed class FlashOfferGetRequestDataModel : IFlashOfferGetRequestModel, INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private uint _quantity;
        private string _radius;
        private DateTime _expireDate;
        private string _priceType;
        private string _period;
        private uint _price;

        public uint Price
        {
            get => _price;
            set
            {
                if (value == _price) return;
                _price = value;
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
        
        public string PriceType
        {
            get => _priceType;
            set
            {
                if (value == _priceType) return;
                _priceType = value;
                OnPropertyChanged();
            }
        }

        public string Period
        {
            get => _period;
            set
            {
                if(_period == value) return;
                _period = value;
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