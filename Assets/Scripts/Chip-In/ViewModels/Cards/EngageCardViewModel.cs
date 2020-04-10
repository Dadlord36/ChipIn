using System;
using System.ComponentModel;
using DataModels;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;
using Views.Cards;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class EngageCardViewModel : CorrespondingViewModel<EngageCardView>, IEngageModel, INotifyPropertyChanged
    {
        public event Action<EngageCardDataModel> CardWasSelected;

        private readonly EngageCardDataModel _cardData = new EngageCardDataModel();

        protected override void OnEnable()
        {
            base.OnEnable();
            RelatedView.WasClicked += OnCardWasClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RelatedView.WasClicked -= OnCardWasClicked;
        }

        private void OnCardWasClicked()
        {
            OnCardWasSelected(_cardData);
        }

        public void FillCardWithData(EngageCardDataModel cardData)
        {
            _cardData.Set(cardData);
        }

        [Binding]
        public string Title
        {
            get => _cardData.Title;
            set => _cardData.Title = value;
        }

        [Binding]
        public string Description
        {
            get => _cardData.Description;
            set => _cardData.Description = value;
        }

        [Binding]
        public string MarketAge
        {
            get => _cardData.MarketAge;
            set => _cardData.MarketAge = value;
        }

        [Binding]
        public string MarketSize
        {
            get => _cardData.MarketSize;
            set => _cardData.MarketSize = value;
        }

        [Binding]
        public string MarketCap
        {
            get => _cardData.MarketCap;
            set => _cardData.MarketCap = value;
        }

        [Binding]
        public string MarketSpirit
        {
            get => _cardData.MarketSpirit;
            set => _cardData.MarketSpirit = value;
        }

        [Binding]
        public Sprite Icon
        {
            get => _cardData.Icon;
            set => _cardData.Icon = value;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _cardData.PropertyChanged += value;
            remove => _cardData.PropertyChanged -= value;
        }

        private void OnCardWasSelected(EngageCardDataModel obj)
        {
            CardWasSelected?.Invoke(obj);
        }
    }
}