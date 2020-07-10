using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngineInternal;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements.Lists.ScrollableList
{
    public class ContentItemOrderController : UIBehaviour, IContentItemUpdater
    {
        public void UpdateContentItem(Transform contentItem, float pathPercentage)
        {
            if (!enabled) return;
            contentItem.SetSiblingIndex((int) (pathPercentage * contentItem.parent.childCount));
        }
    }
}