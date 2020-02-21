using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using ViewModels.UI;

namespace Views.ViewElements.ViewsSwitching
{
    [RequireComponent(typeof(ScrollViewController))]
    public class ViewsAnimatedSwitcher : UIBehaviour
    {
        private ScrollViewController _scrollViewController;

        protected override void OnEnable()
        {
            Assert.IsTrue(TryGetComponent(out _scrollViewController));
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