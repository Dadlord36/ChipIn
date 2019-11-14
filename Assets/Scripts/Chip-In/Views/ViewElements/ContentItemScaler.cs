using UnityEngine;
using UnityEngine.EventSystems;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements
{
    [DisallowMultipleComponent]
    public class ContentItemScaler : UIBehaviour, IContentItemUpdater
    {
        private Vector3 scale;

        public void UpdateContentItem(Transform contentItem, float pathPercentage)
        {
            scale.Set(pathPercentage, pathPercentage, pathPercentage);
            contentItem.localScale = scale;
        }
    }
}