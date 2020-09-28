﻿using System;
using Com.TheFallenGames.OSA.CustomParams;
using UnityEngine;
using Views.ViewElements.ScrollViews.Adapters.Parameters.Interfaces;

namespace Views.ViewElements.ScrollViews.Adapters.Parameters
{
    [Serializable]
    public class RepositoryPagesAdapterParameters : BaseParamsWithPrefab, IRepositoryAdapterParameters
    {
        public RepositoryAdapterParameters repositoryAdapterParameters;
        public int PreFetchedItemsCount => repositoryAdapterParameters.PreFetchedItemsCount;
        public bool FreezeContentEndEdgeOnCountChange => repositoryAdapterParameters.FreezeContentEndEdgeOnCountChange;
        

        public void SetScrollInteractivity(bool activity)
        {
            DragEnabled = activity;
        }
    }
}