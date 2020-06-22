using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataModels.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using Views;

namespace DataModels
{
    public interface IEngageModel : IMarketModel, IIdentifier, INamed, IDescription, IIconSprite
    {
    }

    public sealed class EngageCardDataModel : IEngageModel, INotifyPropertyChanged
    {
        private uint _size;
        private uint _minCap;
        private uint _maxCap;
        private string _age;
        private int? _id;
        private string _name;
        private string _description;
        private Sprite _icon;
        private string _spirit;

        public EngageCardDataModel()
        {
        }

        public EngageCardDataModel(IMarketInterestDetailsDataModel marketInterestDetailsDataModel, Sprite icon)
        {
            Age = marketInterestDetailsDataModel.Age;
            Size = marketInterestDetailsDataModel.Size;
            Description = marketInterestDetailsDataModel.Description;
            Icon = icon;
            Id = marketInterestDetailsDataModel.Id;
            Name = marketInterestDetailsDataModel.Name;
            MaxCap = marketInterestDetailsDataModel.MaxCap;
            MinCap = marketInterestDetailsDataModel.MinCap;
        }

        public void Set(IEngageModel model)
        {
            Age = model.Age;
            Size = model.Size;
            Description = model.Description;
            Icon = model.Icon;
            Id = model.Id;
            Name = model.Name;
            MaxCap = model.MaxCap;
            MinCap = model.MinCap;
        }

        #region IEngageModel implementation

        public uint Size
        {
            get => _size;
            set
            {
                if (value == _size) return;
                _size = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                if (value == _age) return;
                _age = value;
                OnPropertyChanged();
            }
        }

        public uint MinCap
        {
            get => _minCap;
            set
            {
                if (value == _minCap) return;
                _minCap = value;
                OnPropertyChanged();
            }
        }

        public uint MaxCap
        {
            get => _maxCap;
            set
            {
                if (value == _maxCap) return;
                _maxCap = value;
                OnPropertyChanged();
            }
        }
        

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

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
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

        public Sprite Icon
        {
            get => _icon;
            set
            {
                if (Equals(value, _icon)) return;
                _icon = value;
                OnPropertyChanged();
            }
        }

        public string Spirit
        {
            get => _spirit;
            set
            {
                if (value == _spirit) return;
                _spirit = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}