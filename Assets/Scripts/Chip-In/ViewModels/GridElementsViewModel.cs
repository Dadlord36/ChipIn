using System.Collections.Specialized;
using Repositories.Remote;
using UnityEngine;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    public sealed class GridElementsViewModel : BaseViewModel
    {
        [SerializeField] private CommunityInterestRemoteRepository remoteRepository;

        protected override void OnEnable()
        {
            base.OnEnable();
            remoteRepository.DataWasLoaded += UpdateGridContent;
            UpdateGridContent();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            remoteRepository.DataWasLoaded -= UpdateGridContent;
        }

        private void RemoteRepositoryOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateGridContent();
        }

        private void UpdateGridContent()
        {
            var gridView = (GridElementsView) View;
            var itemsData = remoteRepository.ItemsData;
            gridView.ClearItems();

            for (var index = 0; index < itemsData.Count; index++)
            {
                var itemData = itemsData[index];
                gridView.FillOneItemWithData(itemData);
            }
        }
    }
}