using System;
using Com.TheFallenGames.OSA.CustomParams;
using Views.ViewElements.ScrollViews.Adapters.Parameters.Interfaces;

namespace Views.ViewElements.ScrollViews.Adapters.Parameters
{
    [Serializable]
    public class RepositoryPagesAdapterParameters : BaseParamsWithPrefab, IRepositoryAdapterParameters
    {
        public RepositoryAdapterParameters repositoryAdapterParameters;
        public int PreFetchedItemsCount => repositoryAdapterParameters.PreFetchedItemsCount;
        public bool FreezeContentEndEdgeOnCountChange => repositoryAdapterParameters.FreezeContentEndEdgeOnCountChange;
    }
}