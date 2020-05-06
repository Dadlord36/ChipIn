using System.Collections.Specialized;
using Repositories.Remote;
using UnityEngine;
using Views;
using Views.ViewElements;

namespace ViewModels.Basic
{
    public abstract class BaseMenuViewModel<TView> : CorrespondingViewsSwitchingViewModel<TView> where TView : BaseView
    {
        [SerializeField] protected IconsScrollView newItemsScrollView;

        // protected abstract BaseItemsListRepository<T> ItemsRemoteRepository { get; }

        protected override void OnEnable()
        {
            base.OnEnable();
            // SubscribeOnEvents();
            UpdateItems();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            // UnsubscribeFromEvents();
        }


        /*protected abstract void SubscribeOnEvents();
        /*{
            ItemsRemoteRepository.CollectionChanged += ItemsRemoteRepositoryOnCollectionChanged;
            ItemsRemoteRepository.DataWasLoaded += ItemsRemoteRepositoryOnDataWasLoaded;
        }#1#

        protected abstract void UnsubscribeFromEvents();
        /*{
            /*ItemsRemoteRepository.CollectionChanged -= ItemsRemoteRepositoryOnCollectionChanged;
            ItemsRemoteRepository.DataWasLoaded -= ItemsRemoteRepositoryOnDataWasLoaded;#2#
        }#1#*/

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