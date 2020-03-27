using System;
using Behaviours.Games.Interfaces;
using Controllers;
using Repositories.Remote;
using ScriptableObjects.ActionsConnectors;
using UnityEngine;
using UnityEngine.Assertions;

namespace Behaviours.Games
{
    public class CoinsGame : MonoBehaviour, IGame
    {
        [SerializeField] private UserCoinsAmountRepository coinsAmountRepository;
        [SerializeField] private int coinsToPick;

        public event Action GameComplete;
        private int _coinsAmount, _coinsPicked;
        private bool _isInitialized;

        private void Awake()
        {
            Assert.IsNotNull(coinsAmountRepository);
        }

        private void AddCoinsToCoinsRepository(uint amount)
        {
            coinsAmountRepository.Add(amount);
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

            var coins = FindObjectsOfType<Component>();

            for (var i = 0; i < coins.Length; i++)
            {
                if (coins[i] is IInteractiveUintValue interactiveValue)
                {
                    SubscribeToInteractiveValue(interactiveValue);
                }

                if (coins[i] is IFinishingAction finishingAction)
                {
                    SubscribeOnFinishingAction(finishingAction);
                }
            }

            void SubscribeOnFinishingAction(IFinishingAction finishingAction)
            {
                finishingAction.FinishingActionDone += CheckIfGameIsComplete;
            }

            void SubscribeToInteractiveValue(IInteractiveUintValue interactiveValue)
            {
                interactiveValue.GenerateValue();
                interactiveValue.Collected += delegate(uint value)
                {
                    _coinsPicked++;
                    AddCoinsToCoinsRepository(value);
                };
            }
        }
        

        private void CheckIfGameIsComplete()
        {
            if (coinsToPick == _coinsPicked)
            {
                GameComplete?.Invoke();
            }
        }
    }
}