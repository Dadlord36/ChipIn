using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Interfaces;

namespace ViewModels.Cards
{
    [Binding]
    public sealed class CompanyAdFeatureCardViewModel : MonoBehaviour, INotifyPropertyChanged, ICompanyAdFeatureModel
    {
        private string _posterImagePath;
        private string _description;
        private int _tokensRewardAmount;

        [SerializeField] private int featureNumber;
        
        [Binding] public int FeatureNumber => featureNumber;

        [Binding]
        public int TokensRewardAmount
        {
            get => _tokensRewardAmount;
            set
            {
                if (value == _tokensRewardAmount) return;
                _tokensRewardAmount = value;
                OnPropertyChanged();
            }
        }

        [Binding]
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

        [Binding]
        public string PosterImagePath
        {
            get => _posterImagePath;
            set
            {
                if (value == _posterImagePath) return;
                _posterImagePath = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}