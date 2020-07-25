using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Validators;
using ViewModels.Cards;
using ViewModels.Interfaces;
using Views;
using WebOperationUtilities;

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
        [SerializeField] private CompanyAdFeatureCardViewModel[] companyAdFeatureCardViewModels;
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
        public async void PreviewButton_OnClick()
        {
            bool canPreview = true;

            foreach (var cardViewModel in companyAdFeatureCardViewModels)
            {
                if (new CompanyAdFeatureValidator(cardViewModel).IsValid) continue;
                canPreview = false;
                break;
            }

            if (string.IsNullOrEmpty(_companyLogoImagePath) || string.IsNullOrEmpty(_companyPosterImagePath))
            {
                canPreview = false;
            }

            if (!canPreview) return;

            Texture2D texture = null;
            try
            {
                texture = await SpritesUtility.CreateTexture2DFromPathAsync(CompanyPosterImagePath, GameManager.MainThreadScheduler)
                    .ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            finally
            {
                SwitchToView(nameof(CompanyAdPreviewView), new FormsTransitionBundle(new CompanyAdFeaturesPreviewData(
                    Array.ConvertAll(companyAdFeatureCardViewModels, item => (ICompanyAdFeatureModel) item),
                    CompanyLogoImagePath, CompanyPosterImagePath)));
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