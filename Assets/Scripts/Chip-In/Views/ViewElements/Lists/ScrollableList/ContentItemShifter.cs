using UnityEngine;
using UnityEngine.EventSystems;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements.Lists.ScrollableList
{
    [DisallowMultipleComponent]
    public class ContentItemShifter : UIBehaviour, IContentItemUpdater
    {
        [SerializeField] private float maxYOffset;

        public void UpdateContentItem(Transform contentItem, float pathPercentage)
        {
            var localPosition = contentItem.localPosition;
            localPosition.y = Mathf.Lerp(0f, maxYOffset, pathPercentage);
            contentItem.localPosition = localPosition;
        }
    }
}