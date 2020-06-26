using System;
using UnityEngine;
using Views.ViewElements.ScrollViews.Adapters.Parameters.Interfaces;

namespace Views.ViewElements.ScrollViews.Adapters.Parameters
{
    [Serializable]
    public class RepositoryAdapterParameters : IRepositoryAdapterParameters
    {
        [SerializeField] private int preFetchedItemsCount = 5;
        [NonSerialized] private bool freezeContentEndEdgeOnCountChange;

        public int PreFetchedItemsCount => preFetchedItemsCount;
        public bool FreezeContentEndEdgeOnCountChange => freezeContentEndEdgeOnCountChange;
    }
}