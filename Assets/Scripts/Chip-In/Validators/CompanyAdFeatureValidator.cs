using ViewModels.Interfaces;

namespace Validators
{
    public class CompanyAdFeatureValidator
    {
        public readonly bool IsValid;

        public CompanyAdFeatureValidator(ICompanyAdFeatureModel model)
        {
            IsValid = CheckIsValid(model);
        }

        private static bool CheckIsValid(ICompanyAdFeatureModel model)
        {
            return !string.IsNullOrEmpty(model.Description) && !string.IsNullOrEmpty(model.PosterImagePath);
        }
    }
}