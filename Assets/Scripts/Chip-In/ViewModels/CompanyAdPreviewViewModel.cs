using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels.Basic;
using ViewModels.Interfaces;
using Views;
using WebOperationUtilities;

namespace ViewModels
{
    [Binding]
    public sealed class CompanyAdPreviewViewModel : CorrespondingViewsSwitchingViewModel<CompanyAdPreviewView>, INotifyPropertyChanged
    {
        private Sprite _backgroundSprite;
        private int _selectedItemIndex;
        private string _selectedFeatureDescription;

        private ICompanyAdFeatureModel[] _featuresModels;

        [Binding]
        public string SelectedFeatureDescription
        {
            get => _selectedFeatureDescription;
            set
            {
                if (value == _selectedFeatureDescription) return;
                _selectedFeatureDescription = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                if (value == _selectedItemIndex) return;
                _selectedItemIndex = value;
                SelectedFeatureDescription = _featuresModels[value].Description;
                OnPropertyChanged();
            }
        }


        [Binding]
        public Sprite BackgroundSprite
        {
            get => _backgroundSprite;
            set
            {
                _backgroundSprite = value;
                OnPropertyChanged();
            }
        }

        public CompanyAdPreviewViewModel() : base(nameof(CompanyAdPreviewViewModel))
        {
        }


        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            var companyAdFeaturesPreviewData = RelatedView.FormTransitionBundle.TransitionData as CompanyAdFeaturesPreviewData;
            Debug.Assert(companyAdFeaturesPreviewData != null, nameof(companyAdFeaturesPreviewData) + " != null");
            
            if (companyAdFeaturesPreviewData.BackgroundTexture != null)
                BackgroundSprite = SpritesUtility.CreateSpriteWithDefaultParameters(companyAdFeaturesPreviewData.BackgroundTexture);
            
            _featuresModels = companyAdFeaturesPreviewData.FeatureModelsToPreview;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}