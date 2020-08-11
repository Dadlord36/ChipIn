using UnityWeld.Binding;

namespace Validators
{
    [Binding]
    public class TextsEqualityValidator : BaseValidationWithAlert
    {
        [Binding] public string OriginalText { get; set; }
        [Binding] public string ControlText { get; set; }

        protected override bool CheckIsValid()
        {
            return IsValid = OriginalText.Equals(ControlText);
        }
    }
}