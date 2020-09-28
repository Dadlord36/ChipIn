using DataModels.Interfaces;

namespace Validators
{
    public class CompanyAdFeatureValidator
    {
        public readonly bool IsValid;

        public CompanyAdFeatureValidator(IAdvertFeatureBaseModel model)
        {
            IsValid = CheckIsValid(model);
        }

        private static bool CheckIsValid(IAdvertFeatureBaseModel model)
        {
            return !string.IsNullOrEmpty(model.Description) && !string.IsNullOrEmpty(model.Icon);
        }
    }
}