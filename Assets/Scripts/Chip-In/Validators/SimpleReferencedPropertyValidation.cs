using UnityWeld.Binding;

namespace Validators
{
    [Binding]
    public class SimpleReferencedPropertyValidation : BaseValidationWithAlert
    {
        public bool ReferencedValueIsValid
        {
            get => IsValid;
            set => IsValid = value;
        }


        protected override bool CheckIsValid()
        {
            return ReferencedValueIsValid;
        }
    }
}