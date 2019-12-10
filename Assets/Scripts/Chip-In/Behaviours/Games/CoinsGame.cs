using System;
using Behaviours.Games.Interfaces;
using ScriptableObjects.ActionsConnectors;
using UnityEngine;
using UnityEngine.Assertions;

namespace Behaviours.Games
{
    public class CoinsGame : MonoBehaviour, IGame
    {
        [SerializeField] private ValueConnector valueConnector;
        [SerializeField] private int coinsToPick;

        public event Action GameComplete;
        private int _coinsAmount, _coinsPicked;

        private void Awake()
        {
            Assert.IsNotNull(valueConnector);
        }

        public int CoinsAmount
        {
            get => _coinsAmount;
            set
            {
                _coinsAmount = value;
                SubmitValueChange();
            }
        }

        private void SubmitValueChange()
        {
            valueConnector.OnValueChanged(CoinsAmount);
        }

        private void OnEnable()
        {
            var coins = FindObjectsOfType<Component>();

            for (var i = 0; i < coins.Length; i++)
            {
                if (coins[i] is IInteractiveValue interactiveValue)
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
            
            void SubscribeToInteractiveValue(IInteractiveValue interactiveValue)
            {
                interactiveValue.GenerateValue();
                interactiveValue.Collected += delegate(int value)
                {
                    _coinsPicked++;
                    CoinsAmount += value;
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