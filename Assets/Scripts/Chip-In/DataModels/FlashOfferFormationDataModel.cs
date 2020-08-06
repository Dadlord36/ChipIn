using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels.Interfaces;
using JetBrains.Annotations;

namespace DataModels
{
    public class FlashOfferFormationDataModel : INotifyPropertyChanged, IFlashOfferFormationModel
    {
        private int? _id;
        private string _title;
        private string _description;
        private uint _quantity;
        private uint _tokensAmount;
        private string _radius;
        private DateTime _expireDate;
        private string _posterUri;


        public int? Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
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

        public string PosterUri
        {
            get => _posterUri;
            set
            {
                if (value == _posterUri) return;
                _posterUri = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}