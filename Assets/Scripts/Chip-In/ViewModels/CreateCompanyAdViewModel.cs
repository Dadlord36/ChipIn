using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;
using Views;
using WebOperationUtilities;

namespace ViewModels
{
    [Binding]
    public sealed class CreateCompanyAdViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private bool _logoIsSelected;
        private bool _posterIsSelected;

        private CreateCompanyAdView ThisView => View as CreateCompanyAdView;

        [Binding]
        public bool LogoIconSelected
        {
            get => _logoIsSelected;
            set
            {
                if (value == _logoIsSelected) return;
                _logoIsSelected = true;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool PosterIsSelected
        {
            get => _posterIsSelected;
            set
            {
                if (value == _posterIsSelected) return;
                _posterIsSelected = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public void ChoseLogoImage_OnClick()
        {
            NativeGallery.GetImageFromGallery(delegate(string path) { SetLogoIconFromTexture(NativeGallery.LoadImageAtPath(path)); });
        }

        [Binding]
        public void ChosePosterImage_OnClick()
        {
            NativeGallery.GetImageFromGallery(delegate(string path) { SetPosterIconFromTexture(NativeGallery.LoadImageAtPath(path)); });
        }

        private void SetLogoIconFromTexture(Texture2D texture)
        {
            LogoIconSelected = true;
            ThisView.Logo = SpritesUtility.CreateSpriteWithDefaultParameters(texture);
        }

        private void SetPosterIconFromTexture(Texture2D texture)
        {
            PosterIsSelected = true;
            ThisView.CompanyPoster = SpritesUtility.CreateSpriteWithDefaultParameters(texture);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}