using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class ChallengeViewModel : BaseContainerItemsViewModel
    {
        [SerializeField] private ChallengesCardsParametersRepository challengesCardsParametersRepository;
        [SerializeField] private ChallengesRemoteRepository challengesRemoteRepository;
        [SerializeField] private SelectedGameRepository selectedGameRepository;

        private ChallengeView ChallengeView => View as ChallengeView;

        [Binding]
        public void Play_OnButtonClick()
        {
            SwitchToSlotsGameView();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnRepositoryItemsCollectionChangesEvent(challengesRemoteRepository);
            SwitchPlayButtonActivity();
        }

        private void SwitchPlayButtonActivity()
        {
            ChallengeView.PlayButtonInteractivity = selectedGameRepository.GameWasSelected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeOnRepositoryItemsCollectionChangesEvent(challengesRemoteRepository);
        }

        private void SwitchToSlotsGameView()
        {
            SwitchToView(nameof(SlotsGameView));
        }

        private void AddCard(string challengeTypeName, uint coinsAmount)
        {
            var visibleParameters = challengesCardsParametersRepository.GetItemVisibleParameters(challengeTypeName);
            visibleParameters.coinsAmount = coinsAmount;
            var card = ((ChallengeView) View).AddItem();
            card.SetupCardViewElements(visibleParameters);
        }

        protected override void ClearAllItems()
        {
            ((ChallengeView) View).RemoveAllItems();
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