using System.Collections.Specialized;

namespace ViewModels
{
    public abstract class BaseContainerItemsViewModel : ViewsSwitchingViewModel
    {
        protected abstract void ClearAllItems();
        protected abstract void FillContainerWithDataFromRepository();

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            UpdateItemContainer();
        }

        public void SubscribeOnRepositoryItemsCollectionChangesEvent(INotifyCollectionChanged collectionChanged)
        {
            collectionChanged.CollectionChanged += OnRelatedCollectionChanged;
        }

        public void UnsubscribeOnRepositoryItemsCollectionChangesEvent(INotifyCollectionChanged collectionChanged)
        {
            collectionChanged.CollectionChanged -= OnRelatedCollectionChanged;
        }

        private void OnRelatedCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            UpdateItemContainer();
        }

        private void UpdateItemContainer()
        {
            ClearAllItems();
            FillContainerWithDataFromRepository();
        }
    }
}