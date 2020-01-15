using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class GamesViewModel : BaseContainerItemsViewModel
    {
        [SerializeField] private SoloGamesRemoteRepository gamesRemoteRepository;
        [SerializeField] private SoloGameItemParametersRepository itemParametersRepository;

        protected override void OnEnable()
        {
            base.OnEnable();
            SubscribeOnRepositoryItemsCollectionChangesEvent(gamesRemoteRepository);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeOnRepositoryItemsCollectionChangesEvent(gamesRemoteRepository);
        }

        public void ProcessGameLaunch(SingleGameData parameters)
        {
        }

        public void AddSoloGameItem(SingleGameData itemData)
        {
            var gameView = (GamesView) View;
            var soloGameItem = gameView.AddItem();

            var gameItemVisibleParameters =
                itemParametersRepository.GetItemVisibleParameters(itemData.TypeName);

            soloGameItem.GameTypeIcon = gameItemVisibleParameters.gameTypeSprite;
        }

        protected override void ClearAllItems()
        {
            ((GamesView) View).RemoveAllItems();
        }

        protected override void FillContainerWithDataFromRepository()
        {
            foreach (var item in gamesRemoteRepository.ItemsData)
            {
                AddSoloGameItem(item);
            }
        }
    }
}