using UnityEngine;

namespace Views.ViewElements.ScrollViews.Adapters.Parameters.Interfaces
{
    public interface IRepositoryAdapterParameters
    {
        int PreFetchedItemsCount { get; } 
        bool FreezeContentEndEdgeOnCountChange { get; } 
        void SetScrollInteractivity(bool activity);
        RectTransform FetchingLimitUnlockButtonPrefab { get; }
    }
}