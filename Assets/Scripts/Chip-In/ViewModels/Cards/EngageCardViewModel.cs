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
        
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _cardData.PropertyChanged += value;
            remove => _cardData.PropertyChanged -= value;
        }

        private void OnCardWasSelected(EngageCardDataModel obj)
        {
            CardWasSelected?.Invoke(obj);
        }

        #region IEngageModel implementation 

        [Binding]
        public uint Size
        {
            get => _cardData.Size;
            set => _cardData.Size = value;
        }

        [Binding]
        public uint MinCap
        {
            get => _cardData.MinCap;
            set => _cardData.MinCap = value;
        }

        [Binding]
        public uint MaxCap
        {
            get => _cardData.MaxCap;
            set => _cardData.MaxCap = value;
        }

        [Binding]
        public string Age
        {
            get => _cardData.Age;
            set => _cardData.Age = value;
        }

        [Binding]
        public int? Id
        {
            get => _cardData.Id;
            set => _cardData.Id = value;
        }

        [Binding]
        public string Name
        {
            get => _cardData.Name;
            set => _cardData.Name = value;
        }

        [Binding]
        public string Description
        {
            get => _cardData.Description;
            set => _cardData.Description = value;
        }

        [Binding]
        public Sprite Icon
        {
            get => _cardData.Icon;
            set => _cardData.Icon = value;
        }
        
        [Binding]
        public string Spirit
        {
            get => _cardData.Spirit;
            set => _cardData.Spirit = value;
        }

        #endregion

    }
}