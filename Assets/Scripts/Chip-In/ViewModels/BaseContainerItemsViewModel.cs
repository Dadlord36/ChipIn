using System.Collections.Specialized;

namespace ViewModels
{
    public abstract class BaseContainerItemsViewModel : ViewsSwitchingViewModel
    {
        protected abstract void ClearAllItems();
        protected abstract void FillContainerWithDataFromRepository();

        public void SubscribeOnRepositoryItemsCollectionChangesEvent(INotifyCollectionChanged collectionChanged)
        {
            collectionChanged.CollectionChanged += UpdateChallengesView;
        }

        public void UnsubscribeOnRepositoryItemsCollectionChangesEvent(INotifyCollectionChanged collectionChanged)
        {
            collectionChanged.CollectionChanged -= UpdateChallengesView;
        }

        private void UpdateChallengesView(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            ClearAllItems();
            FillContainerWithDataFromRepository();
        }
    }
}