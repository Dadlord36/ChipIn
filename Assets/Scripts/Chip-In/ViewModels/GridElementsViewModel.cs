using System.Collections.Specialized;
using Repositories.Remote;
using UnityEngine;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    public sealed class GridElementsViewModel : BaseViewModel
    {
        [SerializeField] private CommunitiesDataRepository dataRepository;

        protected override void OnEnable()
        {
            base.OnEnable();
            dataRepository.DataWasLoaded += UpdateGridContent;
            UpdateGridContent();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            dataRepository.DataWasLoaded -= UpdateGridContent;
        }

        private void RemoteRepositoryOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateGridContent();
        }

        private void UpdateGridContent()
        {
            var gridView = (GridElementsView) View;
            var itemsData = dataRepository.ItemsData;
            gridView.ClearItems();

            for (var index = 0; index < itemsData.Count; index++)
            {
                gridView.FillOneItemWithData(itemsData[index]);
            }
        }
    }
}