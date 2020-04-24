using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace DataModels
{
    public interface IEngageModel
    {
        string Title { get; set; }
        string Description { get; set; }
        string MarketAge { get; set; }
        string MarketSize { get; set; }
        string MarketCap { get; set; }
        string MarketSpirit { get; set; }
        Sprite Icon { get; set; }
    }

    public class EngageCardDataModel : IEngageModel, INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private string _marketAge;
        private string _marketSize;
        private string _marketCap;
        private string _marketSpirit;
        private Sprite _icon;
        
        public EngageCardDataModel(){}
        
        public EngageCardDataModel(string title, string description, string marketAge, string marketSize, 
            string marketCap, string marketSpirit, Sprite icon)
        {
            _title = title;
            _description = description;
            _marketAge = marketAge;
            _marketSize = marketSize;
            _marketCap = marketCap;
            _marketSpirit = marketSpirit;
            _icon = icon;
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

        public string MarketAge
        {
            get => _marketAge;
            set
            {
                if (value == _marketAge) return;
                _marketAge = value;
                OnPropertyChanged();
            }
        }

        public string MarketSize
        {
            get => _marketSize;
            set
            {
                if (value == _marketSize) return;
                _marketSize = value;
                OnPropertyChanged();
            }
        }

        public string MarketCap
        {
            get => _marketCap;
            set
            {
                if (value == _marketCap) return;
                _marketCap = value;
                OnPropertyChanged();
            }
        }

        public string MarketSpirit
        {
            get => _marketSpirit;
            set
            {
                if (value == _marketSpirit) return;
                _marketSpirit = value;
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

        public void Set(EngageCardDataModel data)
        {
            Title = data.Title;
            Description = data.Description;
            MarketAge = data.MarketAge;
            MarketSize = data.MarketSize;
            MarketCap = data.MarketCap;
            MarketSpirit = MarketSpirit;
            Icon = data.Icon;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}