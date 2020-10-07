using System.ComponentModel;
using UnityWeld.Binding;
using Repositories.Remote;
using UnityEngine;

namespace ViewModels.UI
{
    [Binding]
    public sealed class LogoViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private MerchantProfileSettingsRepository merchantProfileSettingsRepository;
        [SerializeField] private Sprite defaultLogo;
        private IMerchantProfileSettings MerchantProfileSettingsModelImplementation => merchantProfileSettingsRepository;
        
        [Binding]
        public Sprite LogoSprite
        {
            get => MerchantProfileSettingsModelImplementation.LogoSprite
                ? MerchantProfileSettingsModelImplementation.LogoSprite
                : defaultLogo;
            set => MerchantProfileSettingsModelImplementation.LogoSprite = value;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => merchantProfileSettingsRepository.PropertyChanged += value;
            remove => merchantProfileSettingsRepository.PropertyChanged -= value;
        }
    }
}