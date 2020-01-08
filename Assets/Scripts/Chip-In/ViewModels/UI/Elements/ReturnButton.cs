using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ViewModels;

namespace UI.Elements
{
    public class ReturnButton : UIBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            var viewModel = GetComponentInParent<ViewsSwitchingViewModel>();
            var button = GetComponent<Button>();

            Assert.IsNotNull(viewModel);
            Assert.IsNotNull(button);
            
            button.onClick.AddListener(delegate { viewModel.SwitchToPreviousView(); });
        }
    }
}