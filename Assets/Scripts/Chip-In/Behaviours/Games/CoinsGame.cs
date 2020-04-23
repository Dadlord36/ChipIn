using System;
using Behaviours.Games.Interfaces;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

namespace Behaviours.Games
{
    public sealed class CoinsGame : MonoBehaviour, IGame
    {
        private const string Tag = nameof(CoinsGame);
        
        [SerializeField] private UserCoinsAmountRepository coinsAmountRepository;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private int coinsToPick;

        public event Action GameComplete;
        private int _coinsAmount, _coinsPicked;
        private bool _isInitialized;

        private Coin[] _coins;

        private void Awake()
        {
            Assert.IsNotNull(coinsAmountRepository);
        }

        private void UpdateCoinsRepository()
        {
            coinsAmountRepository.UpdateRepositoryData();
        }

        private void OnEnable()
        {
            InitializeCoinsGame();
            _coinsPicked = 0;
        }

        private void InitializeCoinsGame()
        {
            if (_isInitialized) return;
            _isInitialized = true;

            _coins = FindObjectsOfType<Coin>();

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
                collectable.WasCollected += async delegate(IInteractiveUintValue value)
                {
                    try
                    {
                        _coinsPicked++;
                        var result = await CoinsMiniGameStaticProcessor.TossACoin(authorisationDataRepository);

                        if (!result.Success)
                        {
                            LogUtility.PrintLog(Tag, "Failed to toss a coin");
                            return;
                        }

                        var responseInterface = result.ResponseModelInterface;
                        value.SetValue(responseInterface.CoinsTossingResultData.NewCoins);
                        UpdateCoinsRepository();
                    }
                    catch (Exception e)
                    {
                        LogUtility.PrintLogException(e);
                        throw;
                    }
                };
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