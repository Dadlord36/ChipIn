using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Interfaces;
using Views;

namespace ViewModels
{
    public class CompanyAdFeaturesPreviewData
    {
        public readonly string CompanyLogoImagePath;
        public readonly string CompanyPosterImagePath;
        public readonly ICompanyAdFeatureModel[] FeatureModelsToPreview;

        public CompanyAdFeaturesPreviewData(ICompanyAdFeatureModel[] featureModelsToPreview, string companyLogoImagePath, string companyPosterImagePath)
        {
            FeatureModelsToPreview = featureModelsToPreview;
            CompanyLogoImagePath = companyLogoImagePath;
            CompanyPosterImagePath = companyPosterImagePath;
        }
    }

    [Binding]
    public sealed class CreateCompanyAdViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        private string _companyLogoImagePath;
        private string _companyPosterImagePath;

        [Binding]
        public string CompanyPosterImagePath
        {
            get => _companyPosterImagePath;
            set
            {
                if (value == _companyPosterImagePath) return;
                _companyPosterImagePath = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string CompanyLogoImagePath
        {
            get => _companyLogoImagePath;
            set
            {
                if (value == _companyLogoImagePath) return;
                _companyLogoImagePath = value;
                OnPropertyChanged();
            }
        }

        public CreateCompanyAdViewModel() : base(nameof(CreateCompanyAdViewModel))
        {
        }

        [Binding]
        public void PreviewButton_OnClick()
        {
            /*if (!ValidationHelper.CheckIfAllFieldsAreValid(this))
            {
                return;
            }*/

            SwitchToView(nameof(CompanyAdPreviewView), new FormsTransitionBundle(new CompanyAdFeaturesPreviewData(
                GetComponentsInChildren<ICompanyAdFeatureModel>(), CompanyLogoImagePath, CompanyPosterImagePath)));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}