using System;
using System.Threading.Tasks;
using Behaviours.Games.Interfaces;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

namespace Behaviours.Games
{
    public sealed class CoinsGame : AsyncOperationsMonoBehaviour, IGame
    {
        private const string Tag = nameof(CoinsGame);

        [SerializeField] private UserCoinsAmountRepository coinsAmountRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private int coinsToPick;

        public event Action GameComplete;
        private int _coinsPicked;
        private bool _isInitialized;
        private uint _coinsAmount;

        private Coin[] _coins;

        private void Awake()
        {
            Assert.IsNotNull(coinsAmountRepository);
        }

        private async void OnEnable()
        {
            try
            {
                await InitializeCoinsGame();
                _coinsPicked = 0;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private Task UpdateCoinsRepository()
        {
            return coinsAmountRepository.UpdateRepositoryData();
        }

        private async Task InitializeCoinsGame()
        {
            if (_isInitialized) return;
            _isInitialized = true;

            _coins = FindObjectsOfType<Coin>();
            try
            {
                await TossACoin();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }

            for (var i = 0; i < _coins.Length; i++)
            {
                if (_coins[i] is IFinishingAction finishingAction)
                {
                    SubscribeOnFinishingAction(finishingAction);
                }

                if (_coins[i] is ICollectable collectable)
                {
                    SubscribeToCollectable(collectable);
                }
            }

            void SubscribeOnFinishingAction(IFinishingAction finishingAction)
            {
                finishingAction.FinishingActionDone += CheckIfGameIsComplete;
            }

            void SubscribeToCollectable(ICollectable collectable)
            {
                collectable.WasCollected += delegate(IInteractiveUintValue value)
                {
                    _coinsPicked++;
                    value.SetValue(_coinsAmount);
                };
            }
        }

        private async Task TossACoin()
        {
            try
            {
                var result = await CoinsMiniGameStaticProcessor.TossACoin(out TasksCancellationTokenSource, authorisationDataRepository)
                    .ConfigureAwait(false);

                if (!result.Success)
                {
                    LogUtility.PrintLog(Tag, "Failed to toss a coin");
                    return;
                }

                var responseInterface = result.ResponseModelInterface;
                _coinsAmount = responseInterface.CoinsTossingResultData.NewCoins;

                await UpdateCoinsRepository();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        private void LockCoins()
        {
            foreach (var component in _coins)
            {
                ((ILockable) component).Lock();
            }
        }

        private void UnlockCoins()
        {
            foreach (var component in _coins)
            {
                ((ILockable) component).Unlock();
            }
        }

        private void CheckIfGameIsComplete()
        {
            if (coinsToPick == _coinsPicked)
            {
                CompleteTheGame();
            }
        }

        private void CompleteTheGame()
        {
            LockCoins();
            OnGameComplete();
        }

        private void OnGameComplete()
        {
            GameComplete?.Invoke();
        }
    }
}