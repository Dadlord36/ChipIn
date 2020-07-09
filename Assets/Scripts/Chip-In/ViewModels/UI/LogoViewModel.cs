using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Controllers;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.UI
{
    [Binding]
    public sealed class LogoViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private ViewsLogoController logoController;
        private Sprite _logoSprite;

        
        [Binding]
        public Sprite LogoSprite
        {
            get => _logoSprite;
            private set
            {
                _logoSprite = value;
                OnPropertyChanged();
            }
        }

        private void Awake()
        {
            LogoSprite = logoController.LogoSpite;
        }

        private void OnEnable()
        {
            logoController.LogoChanged += SetLogo;
        }

        private void OnDisable()
        {
            logoController.LogoChanged -= SetLogo;
        }
        
        private void SetLogo(Sprite logoSprite)
        {
            LogoSprite = logoSprite;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}