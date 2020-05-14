using UnityEngine;
using UnityEngine.EventSystems;
using Views.ViewElements.Interfaces;

namespace Views.ViewElements.Lists.ScrollableList
{
    public class ContentItemOrderController : UIBehaviour, IContentItemUpdater
    {
        [SerializeField] private float minOrder, maxOrder;


        public void UpdateContentItem(Transform contentItem, float pathPercentage)
        {
            if (!enabled) return;
            var localPosition = contentItem.transform.localPosition;
            localPosition.z = Mathf.Round(Mathf.Lerp(minOrder, maxOrder, pathPercentage));
            contentItem.transform.localPosition = localPosition;
        }
    }
}