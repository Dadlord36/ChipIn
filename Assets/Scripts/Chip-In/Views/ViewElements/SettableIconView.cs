using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Tasking;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityWeld.Binding;
using WebOperationUtilities;

namespace Views.ViewElements
{
    [Binding]
    public sealed class SettableIconView : UIBehaviour, INotifyPropertyChanged
    {
        public UnityEvent iconWasSelectedFromGallery;
        public UnityEvent iconWasChanged;

        private bool _iconIsSelected;
        private string _selectedImagePath;
        private Sprite _selectedImageSprite;

        [Binding]
        public string SelectedImagePath
        {
            get => _selectedImagePath;
            set
            {
                if (value == _selectedImagePath) return;
                _selectedImagePath = value;

                OnPropertyChanged();
            }
        }


        [Binding]
        public Sprite SelectedImageSprite
        {
            get => _selectedImageSprite;
            set
            {
                _selectedImageSprite = value;
                IconIsSelected = _selectedImageSprite != null;
                OnPropertyChanged();
                OnIconWasChanged();
            }
        }

        [Binding] public Texture2D SelectedImageTexture2D => _selectedImageSprite.texture;

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

        [Binding]
        public void IconPlaceholder_OnClick()
        {
            FillIconWithImageFromGallery();
        }

        private void FillIconWithImageFromGallery()
        {
            NativeGallery.GetImageFromGallery(delegate(string path)
            {
                SelectedImagePath = path;

                if (string.IsNullOrEmpty(SelectedImagePath)) return;
                SelectedImageSprite = SpritesUtility.CreateSpriteWithDefaultParameters(NativeGallery.LoadImageAtPath(_selectedImagePath));
                if (SelectedImageSprite != null)
                {
                    OnIconWasSelectedFromGallery();
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            TasksFactories.ExecuteOnMainThread(() => { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }

        private void OnIconWasSelectedFromGallery()
        {
            TasksFactories.ExecuteOnMainThread(() => { iconWasSelectedFromGallery?.Invoke(); });
        }

        private void OnIconWasChanged()
        {
            TasksFactories.ExecuteOnMainThread(() => { iconWasChanged?.Invoke(); });
        }
    }
}