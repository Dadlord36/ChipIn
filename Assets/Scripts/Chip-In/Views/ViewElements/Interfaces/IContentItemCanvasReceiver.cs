using UnityEngine;

namespace Views.ViewElements.Interfaces
{
    public interface IContentItemCanvasReceiver
    {
        void UpdateContentItemCanvas(Transform contentItem, Canvas canvas, float pathPercentage);
    }
}