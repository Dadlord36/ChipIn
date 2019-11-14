using UnityEngine;

namespace Views.ViewElements.Interfaces
{
    public interface IContentItemUpdater
    {
        void UpdateContentItem(Transform contentItem, float pathPercentage);
    }
}