using Behaviours.Games;
using UnityEngine;
using UnityEngine.Assertions;
using Views;

namespace ViewModels
{
    public class CoinsGameViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private Component miniGame;

        private void Awake()
        {
            Assert.IsNotNull(miniGame);
        }

        private void OnEnable()
        {
            var game = miniGame as IGame;
            Assert.IsNotNull(game);
            game.GameComplete += SwitchToMarketplace;
        }

        private void SwitchToMarketplace()
        {
            SwitchToView(nameof(MarketplaceView));
        }
    }
}