using UnityEngine;
using UnityEngine.EventSystems;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements.ScrollableList
{
    [DisallowMultipleComponent]
    public class ContentItemScaler : UIBehaviour, IContentItemUpdater
    {
        [SerializeField] private float minScale, maxScale;

        private float clampedPercentage;
        private Vector3 tempScale;

        public void UpdateContentItem(Transform contentItem, float pathPercentage)
        {
            clampedPercentage = Mathf.Clamp(pathPercentage, minScale, maxScale);
            tempScale.Set(clampedPercentage, clampedPercentage, clampedPercentage);
            contentItem.localScale = tempScale;
        }
    }
}