using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Controllers;
using DataModels;
using DataModels.Interfaces;
using JetBrains.Annotations;
using UnityWeld.Binding;
using Utilities;
using Views;

namespace ViewModels
{
    public class CompanyAdFeaturesPreviewData
    {
        public readonly string CompanyLogoImagePath;
        public readonly string CompanyPosterImagePath;
        public readonly IReadOnlyList<IAdvertFeatureBaseModel> FeatureModelsToPreview;

        public CompanyAdFeaturesPreviewData(IReadOnlyList<IAdvertFeatureBaseModel> featureModelsToPreview, string companyLogoImagePath, string companyPosterImagePath)
        {
            FeatureModelsToPreview = featureModelsToPreview;
            CompanyLogoImagePath = companyLogoImagePath;
            CompanyPosterImagePath = companyPosterImagePath;
        }

        public CompanyAdFeaturesPreviewData(IAdvertItemModel advertItemDataModel)
        {
            FeatureModelsToPreview = advertItemDataModel.AdvertFeatures as IReadOnlyList<IAdvertFeatureBaseModel>;
            CompanyLogoImagePath = advertItemDataModel.LogoUrl;
            CompanyPosterImagePath = advertItemDataModel.PosterUri;
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

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            Clear();
        }

        private void Clear()
        {
            CompanyLogoImagePath = string.Empty;
            CompanyPosterImagePath = string.Empty;

            foreach (var clearable in GetComponentsInChildren<IClearable>())
            {
                clearable.Clear();
            }
        }

        [Binding]
        public void PreviewButton_OnClick()
        {
            if (!ValidationHelper.CheckIfAllFieldsAreValid(this))
            {
                return;
            }

            SwitchToView(nameof(CompanyAdPreviewView), new FormsTransitionBundle(new CompanyAdFeaturesPreviewData(
                GetComponentsInChildren<IAdvertFeatureBaseModel>(), CompanyLogoImagePath, CompanyPosterImagePath)));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}