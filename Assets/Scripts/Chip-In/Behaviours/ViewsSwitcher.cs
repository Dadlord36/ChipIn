using ScriptableObjects;
using UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Behaviours
{
    [RequireComponent(typeof(ScrollViewController))]
    public class ViewsSwitcher : UIBehaviour
    {
        [SerializeField] private ViewsSwitcherBinding viewsSwitcherBinding;

        private ScrollViewController _scrollViewController;

        protected override void OnEnable()
        {
            Assert.IsTrue(TryGetComponent(out _scrollViewController));
            viewsSwitcherBinding.ViewSwitchingRequested += SwitchViews;
        }

        protected override void OnDisable()
        {
            viewsSwitcherBinding.ViewSwitchingRequested -= SwitchViews;
        }

        private void SwitchViews(ViewsSwitcherBinding.ViewsSwitchData viewsSwitchData)
        {
            _scrollViewController.BeginScrollForward();
        }
    }
}