using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ViewModels.SwitchingControllers;

namespace ViewModels.UI.Elements
{
    public class ReturnButton : UIBehaviour
    {
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;

        protected override void Awake()
        {
            base.Awake();
            
            var button = GetComponent<Button>();
            AssertIsNotNullComponent(button);

            button.onClick.AddListener(delegate { viewsSwitchingController.SwitchToPreviousView(); });
        }

        private void AssertIsNotNullComponent<T>(T component) where T : Component
        {
            Assert.IsNotNull(component, $"There is no {nameof(T)} on this Form");
        }
    }
}