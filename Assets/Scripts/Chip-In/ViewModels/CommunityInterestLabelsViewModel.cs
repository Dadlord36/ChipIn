using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Views.ViewElements;
using NotifyCollectionChangedEventArgs = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace ViewModels
{
    [Binding]
    public class CommunityInterestLabelsViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private CommunityInterestRemoteRepository communityInterestRemoteRepository;
        [SerializeField] private IconsScrollView newItemsScrollView;

        [Binding]
        public void Button_StartAnInterest_OnClick()
        {
            
        }
        
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
            communityInterestRemoteRepository.CollectionChanged += CommunityInterestRemoteRepositoryOnCollectionChanged;
            communityInterestRemoteRepository.DataWasLoaded += CommunityInterestRemoteRepositoryOnDataWasLoaded;
        }

        private void UnsubscribeFromEvents()
        {
            communityInterestRemoteRepository.CollectionChanged -= CommunityInterestRemoteRepositoryOnCollectionChanged;
            communityInterestRemoteRepository.DataWasLoaded -= CommunityInterestRemoteRepositoryOnDataWasLoaded;
        }

        private void CommunityInterestRemoteRepositoryOnDataWasLoaded()
        {
            UpdateItems();
        }

        private void CommunityInterestRemoteRepositoryOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            UpdateItems();
        }

        private void UpdateItems()
        {
            newItemsScrollView.RemoveAllItems();
            var itemsData = communityInterestRemoteRepository.ItemsData;
            for (int i = 0; i < itemsData.Count; i++)
            {
                newItemsScrollView.AddElement(itemsData[i].IconTextureData);
            }
        }
    }
}