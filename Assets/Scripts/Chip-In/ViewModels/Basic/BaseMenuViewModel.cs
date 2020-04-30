using System.Collections.Specialized;
using Repositories.Remote;
using UnityEngine;
using Views.ViewElements;

namespace ViewModels.Basic
{
    public abstract class BaseMenuViewModel<T> : ViewsSwitchingViewModel
    {
        [SerializeField] protected IconsScrollView newItemsScrollView;

        protected abstract BaseItemsListRepository<T> ItemsRemoteRepository { get; }

    protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnEvents();
            UpdateItems();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvents();
        }
        
        private void SubscribeOnEvents()
        {
            ItemsRemoteRepository.CollectionChanged += ItemsRemoteRepositoryOnCollectionChanged;
            ItemsRemoteRepository.DataWasLoaded += ItemsRemoteRepositoryOnDataWasLoaded;
        }

        private void UnsubscribeFromEvents()
        {
            ItemsRemoteRepository.CollectionChanged -= ItemsRemoteRepositoryOnCollectionChanged;
            ItemsRemoteRepository.DataWasLoaded -= ItemsRemoteRepositoryOnDataWasLoaded;
        }
        
        protected virtual void UpdateItems()
        {
            newItemsScrollView.RemoveAllItems();
        }
        
        private void ItemsRemoteRepositoryOnDataWasLoaded()
        {
            UpdateItems();
        }

        private void ItemsRemoteRepositoryOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            UpdateItems();
        }
    }
}