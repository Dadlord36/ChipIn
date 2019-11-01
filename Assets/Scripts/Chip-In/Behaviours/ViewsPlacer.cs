using ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;

namespace Behaviours
{
    public class ViewsPlacer : MonoBehaviour
    {
        [SerializeField] private ViewsSwitcherBinding viewsSwitcherBinding;

        private RectTransform _previousViewContainer, _nextViewContainer;
        private const string PreviousContainerName = "PreviousViewContainer", NextContainerName = "NextViewContainer";

        private void Awake()
        {
            _previousViewContainer = FindOrAttach(PreviousContainerName);
            _nextViewContainer = FindOrAttach(NextContainerName);
            viewsSwitcherBinding.ViewSwitchingRequested += ReplaceCurrentViewsWithGiven;
        }

        private void ReplaceCurrentViewsWithGiven(ViewsSwitcherBinding.ViewsSwitchData viewsSwitchData)
        {
            PlaceInPreviousContainer(viewsSwitchData.fromViewModel.ViewRootRectTransform);
            PlaceInNextContainer(viewsSwitchData.toViewModel.ViewRootRectTransform);
        }

        private RectTransform FindOrAttach(in string objectName)
        {
            var foundObject = gameObject.transform.Find(objectName);

            RectTransform GetRectTransform(Transform owner)
            {
                Assert.IsNotNull(owner);
                Assert.IsTrue(owner.TryGetComponent(out RectTransform rectTransform));
                return rectTransform;
            }

            if (foundObject)
                return GetRectTransform(foundObject);

            foundObject = new GameObject(objectName).transform;
            foundObject.SetParent(transform);
            return GetRectTransform(foundObject);
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
            newChild.offsetMin = newChild.offsetMax = Vector2.zero;
        }
    }
}