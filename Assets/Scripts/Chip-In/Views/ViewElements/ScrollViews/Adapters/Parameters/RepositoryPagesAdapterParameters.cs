using System;
using Com.TheFallenGames.OSA.CustomParams;
using UnityEngine;
using Views.ViewElements.ScrollViews.Adapters.Parameters.Interfaces;

namespace Views.ViewElements.ScrollViews.Adapters.Parameters
{
    [Serializable]
    public class RepositoryPagesAdapterParameters : BaseParamsWithPrefab, IRepositoryAdapterParameters
    {
        [SerializeField] private RectTransform fetchingLimitUnlockButtonPrefab;
        public RepositoryAdapterParameters repositoryAdapterParameters;
        public int PreFetchedItemsCount => repositoryAdapterParameters.PreFetchedItemsCount;
        public bool FreezeContentEndEdgeOnCountChange => repositoryAdapterParameters.FreezeContentEndEdgeOnCountChange;
        public RectTransform FetchingLimitUnlockButtonPrefab => fetchingLimitUnlockButtonPrefab;

        public void SetScrollInteractivity(bool activity)
        {
            DragEnabled = activity;
        }
    }
}