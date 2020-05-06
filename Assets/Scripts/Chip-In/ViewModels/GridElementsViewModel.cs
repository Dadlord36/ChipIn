using System.Collections.Generic;
using DataModels.Interfaces;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    public sealed class GridElementsViewModel : BaseViewModel
    {
        public void UpdateGridContent(IReadOnlyList<IIndexedNamedPosterUrl> dataRepositoryItems)
        {
            var gridView = (GridElementsView) View;

            if (dataRepositoryItems == null) return;

            gridView.ClearItems();

            for (var index = 0; index < dataRepositoryItems.Count; index++)
            {
                gridView.FillOneItemWithData(dataRepositoryItems[index]);
            }
        }
    }
}