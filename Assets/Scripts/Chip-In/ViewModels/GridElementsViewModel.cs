using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.Interfaces;
using Utilities;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    public sealed class GridElementsViewModel : BaseViewModel
    {
        public async Task UpdateGridContent(IReadOnlyList<IIndexedNamedPosterUrl> dataRepositoryItems)
        {
            var gridView = (GridElementsView) View;

            if (dataRepositoryItems == null) return;

            gridView.ClearItems();

            try
            {
                for (var index = 0; index < dataRepositoryItems.Count; index++)
                {
                   await gridView.FillOneItemWithData(dataRepositoryItems[index]);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}