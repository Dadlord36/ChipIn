using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using WebOperationUtilities;

namespace Views.ViewElements
{
    [Binding]
    public sealed class SettableIconView : BaseView, INotifyPropertyChanged
    {
        [SerializeField] private Image iconImage;

        public event Action<string> IconWasSelectedFromGallery;

        private bool _iconIsSelected;

        private Sprite IconSprite
        {
            get => iconImage.sprite;
            set => iconImage.sprite = value;
        }

        [Binding]
        public bool IconIsSelected
        {
            get => _iconIsSelected;
            private set
            {
                if (value == _iconIsSelected) return;
                _iconIsSelected = value;
                OnPropertyChanged();
            }
        }

        public SettableIconView() : base(nameof(SettableIconView))
        {
        }

        [Binding]
        public void IconPlaceholder_OnClick()
        {
            FillIconWithImageFromGallery();
        }

        private void FillIconWithImageFromGallery()
        {
            NativeGallery.GetImageFromGallery(delegate(string path)
            {
                SetIconFromTexture(NativeGallery.LoadImageAtPath(path));
                OnIconWasSelectedFromGallery(path);
            });
        }

        private void SetIconFromTexture(Texture2D texture)
        {
            IconSprite = SpritesUtility.CreateSpriteWithDefaultParameters(texture);
            IconIsSelected = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnIconWasSelectedFromGallery(string obj)
        {
            IconWasSelectedFromGallery?.Invoke(obj);
        }
    }
}