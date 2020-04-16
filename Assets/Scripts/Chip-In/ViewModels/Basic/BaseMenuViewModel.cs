using System.Collections.Specialized;
using Repositories.Remote;
using UnityEngine;
using Views.ViewElements;

namespace ViewModels.Basic
{
    public abstract class BaseMenuViewModel<T> : ViewsSwitchingViewModel
    {
        [SerializeField] protected BaseItemsListRepository<T> itemsRemoteRepository;
        [SerializeField] protected IconsScrollView newItemsScrollView;
        
        
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
            itemsRemoteRepository.CollectionChanged += ItemsRemoteRepositoryOnCollectionChanged;
            itemsRemoteRepository.DataWasLoaded += ItemsRemoteRepositoryOnDataWasLoaded;
        }

        private void UnsubscribeFromEvents()
        {
            itemsRemoteRepository.CollectionChanged -= ItemsRemoteRepositoryOnCollectionChanged;
            itemsRemoteRepository.DataWasLoaded -= ItemsRemoteRepositoryOnDataWasLoaded;
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