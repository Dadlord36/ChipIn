using System;
using Behaviours.Games;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;
using Views;

namespace ViewModels
{
    public class CoinsGameViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private Component miniGame;

        public CoinsGameViewModel() : base(nameof(CoinsGameViewModel))
        {
        }

        private void Awake()
        {
            Assert.IsNotNull(miniGame);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            var game = miniGame as IGame;
            Assert.IsNotNull(game);
            game.GameComplete += SwitchToMarketplace;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvents();
        }

        private void UnsubscribeFromEvents()
        {
            var game = miniGame as IGame;
            Assert.IsNotNull(game);
            game.GameComplete -= SwitchToMarketplace;
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                await ((IGame) miniGame).InitializeCoinsGame();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
           
        }

        private void DestroyMiniGame()
        {
            Destroy(miniGame.gameObject);
        }

        private void SwitchToMarketplace()
        {
            SwitchToView(nameof(MarketplaceView));
            UnsubscribeFromEvents();
        }
    }
}