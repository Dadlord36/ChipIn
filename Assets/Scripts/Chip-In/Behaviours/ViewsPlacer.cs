using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Behaviours
{
    public class ViewsPlacer : MonoBehaviour
    {
        [SerializeField] private ViewsSwitcherBinding viewsSwitcherBinding;

        private RectTransform _previousViewContainer, _nextViewContainer;
        private const string PreviousContainerName = "PreviousViewContainer", NextContainerName = "NextViewContainer";

        private void Awake()
        {
            _previousViewContainer = GameObjectsUtility.FindOrAttach<RectTransform>(transform, PreviousContainerName);
            _nextViewContainer = GameObjectsUtility.FindOrAttach<RectTransform>(transform, NextContainerName);
            viewsSwitcherBinding.ViewSwitchingRequested += ReplaceCurrentViewsWithGiven;
        }

        private void ReplaceCurrentViewsWithGiven(ViewsSwitcherBinding.ViewsSwitchData viewsSwitchData)
        {
            PlaceInPreviousContainer(viewsSwitchData.fromViewModel.ViewRootRectTransform);
            PlaceInNextContainer(viewsSwitchData.toViewModel.ViewRootRectTransform);
        }
        
        public void PlaceInPreviousContainer(RectTransform child)
        {
            ReplaceChild(_previousViewContainer, child);
        }

        public void PlaceInNextContainer(RectTransform child)
        {
            ReplaceChild(_nextViewContainer, child);
        }

        private static void ReplaceChild(RectTransform container, RectTransform newChild)
        {
            container.DetachChildren();
            newChild.SetParent(container);
            newChild.anchorMin = Vector2.zero;
            newChild.anchorMax = Vector2.one;

            newChild.offsetMin = newChild.offsetMax = Vector2.zero;
            newChild.localScale = Vector3.one;
        }
    }
}