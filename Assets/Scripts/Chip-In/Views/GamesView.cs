using UnityEngine;
using UnityEngine.Assertions;

namespace Views
{
    public class GamesView : BaseView
    {
        
        [SerializeField] private Transform gameItemsContainer;
        [SerializeField] private SoloGameItemView soloGameItemPrefab;

        public SoloGameItemView AddSoloGameItem()
        {
            Assert.IsNotNull(gameItemsContainer);

            return Instantiate(soloGameItemPrefab, gameItemsContainer);
        }
    }
}