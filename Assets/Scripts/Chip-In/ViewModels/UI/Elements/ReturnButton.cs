using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;
using ViewModels.SwitchingControllers;

namespace ViewModels.UI.Elements
{
    public class ReturnButton : UIBehaviour
    {
        private const string Tag = nameof(ReturnButton);
        
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;
        [SerializeField] private ViewsSwitchingAnimationBinding viewsSwitchingAnimationBinding;
        private readonly ViewsSwitchingParameters _defaultParameters = new ViewsSwitchingParameters(
            new ViewAppearanceParameters(ViewAppearanceParameters.Appearance.MoveOut, false,
               ViewAppearanceParameters.SwitchingViewPosition.Above, MoveDirection.Right),
            new ViewAppearanceParameters(ViewAppearanceParameters.Appearance.MoveIn, false, 
                ViewAppearanceParameters.SwitchingViewPosition.Under, MoveDirection.Left));


        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnEvents();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(SwitchToPreviousView);
            button.onClick.AddListener(PrintLog);
        }

        private void UnsubscribeFromEvents()
        {
            var button = GetComponent<Button>();
            button.onClick.RemoveListener(SwitchToPreviousView);
            button.onClick.RemoveListener(PrintLog);
        }

        void PrintLog()
        {
            LogUtility.PrintLog(Tag, "Button was clicked");
        }
        
        public void SwitchToPreviousView()
        {
            viewsSwitchingController.SwitchToPreviousView();
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(_defaultParameters);
        }
    }
}