using System.ComponentModel;
using DataModels;
using DataModels.Interfaces;
using DataModels.SimpleTypes;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.ViewElements;

namespace ViewModels
{
    [Binding]
    public class FlashOfferViewModel : ViewsSwitchingViewModel, IFlashOfferModel, INotifyPropertyChanged
    {
        private const string Tag = nameof(FlashOfferViewModel);
        
        private readonly FlashOfferDataModel _flashOfferData = new FlashOfferDataModel();

        [SerializeField] private SettableIconView posterIconView;

        protected override void OnEnable()
        {
            base.OnEnable();
            posterIconView.IconWasSelectedFromGallery += PosterIconViewOnIconWasSelectedFromGallery;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            posterIconView.IconWasSelectedFromGallery -= PosterIconViewOnIconWasSelectedFromGallery;
        }

        private void PosterIconViewOnIconWasSelectedFromGallery(string pathToImageFromGallery)
        {
            PosterFilePath = new FilePath(pathToImageFromGallery);
        }


        [Binding]
        public string Title
        {
            get => _flashOfferData.Title;
            set => _flashOfferData.Title = value;
        }

        [Binding]
        public string Description
        {
            get => _flashOfferData.Description;
            set => _flashOfferData.Description = value;
        }

        [Binding]
        public uint Quantity
        {
            get => _flashOfferData.Quantity;
            set => _flashOfferData.Quantity = value;
        }

        [Binding]
        public int TokensAmount
        {
            get => _flashOfferData.TokensAmount;
            set => _flashOfferData.TokensAmount = value;
        }

        public FilePath PosterFilePath
        {
            get => _flashOfferData.PosterFilePath;
            set => _flashOfferData.PosterFilePath = value;
        }

        [Binding]
        public void Create_OnClick()
        {
            LogUtility.PrintLog(Tag, "Create was clicked");
        }
        
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _flashOfferData.PropertyChanged += value;
            remove => _flashOfferData.PropertyChanged -= value;
        }
    }
}