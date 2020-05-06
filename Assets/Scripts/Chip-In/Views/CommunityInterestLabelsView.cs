using System.Collections.Generic;
using DataModels.Interfaces;
using UnityEngine;
using ViewModels;

namespace Views
{
    public class CommunityInterestLabelsView : BaseView
    {
        [SerializeField] private GridElementsViewModel gridElementsViewModel;

        public void UpdateGridItemsContent(IReadOnlyList<IIndexedNamedPosterUrl> dataRepositoryItems)
        {
            gridElementsViewModel.UpdateGridContent(dataRepositoryItems);
        }
    }
}