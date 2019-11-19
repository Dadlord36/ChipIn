using UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Views.ViewElements.ViewsSwitching
{
    [RequireComponent(typeof(ScrollViewController))]
    public class ViewsSwitcher : UIBehaviour
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
            _scrollViewController.BeginScrollForward();
        }
    }
}