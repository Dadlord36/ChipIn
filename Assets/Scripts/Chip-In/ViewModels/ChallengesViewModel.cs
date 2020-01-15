using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class ChallengesViewModel : BaseContainerItemsViewModel
    {
        [SerializeField] private ChallengesCardsParametersRepository challengesCardsParametersRepository;
        [SerializeField] private ChallengesRemoteRepository challengesRemoteRepository;

        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnRepositoryItemsCollectionChangesEvent(challengesRemoteRepository);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeOnRepositoryItemsCollectionChangesEvent(challengesRemoteRepository);
        }

        private void AddCard(string challengeTypeName, uint coinsAmount)
        {
            var visibleParameters = challengesCardsParametersRepository.GetItemVisibleParameters(challengeTypeName);
            visibleParameters.coinsAmount = coinsAmount;
            var card = ((ChallengesView) View).AddItem();
            card.SetupCardViewElements(visibleParameters);
        }

        protected override void ClearAllItems()
        {
            ((ChallengesView)View).RemoveAllItems(); 
        }

        protected override void FillContainerWithDataFromRepository()
        {
            foreach (var item in challengesRemoteRepository.ItemsData)
            {
                AddCard(item.ChallengeTypeName, item.CoinsPrice);
            }
        }
    }
}