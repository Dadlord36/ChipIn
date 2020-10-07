using UnityWeld.Binding;

namespace ViewModels.Basic
{
    [Binding]
    public sealed class OptionSelectionViewModel : BaseOptionsSelectionViewModel<int>
    {
        public OptionSelectionViewModel() : base(nameof(OptionSelectionViewModel))
        {
        }
    }
}