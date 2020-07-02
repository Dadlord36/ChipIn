using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using ViewModels.UI;

namespace Views.ViewElements.ViewsSwitching
{
    [RequireComponent(typeof(ViewsSwitchingAnimationController))]
    public class ViewsAnimatedSwitcher : UIBehaviour
    {
        private ViewsSwitchingAnimationController viewsSwitchingAnimationController;

        protected override void OnEnable()
        {
            Assert.IsTrue(TryGetComponent(out viewsSwitchingAnimationController));
        }

        private void HideView(BaseView view)
        {
            view.Hide();
        }

        public void SwitchViews()
        {
            // _scrollViewController.BeginScrollForward();
        }
    }
}