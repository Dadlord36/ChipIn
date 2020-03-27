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

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            Destroy(miniGame.gameObject);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
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