using UnityWeld.Binding;
using ViewModels.Basic;

namespace ViewModels
{
    [Binding]
    public class SoloGameItemViewModel : BaseViewModel
    {
        public SoloGameItemViewModel() : base(nameof(SoloGameItemViewModel))
        {
        }

        [Binding]
        public void OnLaunchButtonClick()
        {
        }
    }
}