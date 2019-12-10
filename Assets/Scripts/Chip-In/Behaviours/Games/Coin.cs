using System;
using Behaviours.Games.Interfaces;
using Common.ValueGenerators;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Behaviours.Games
{
    public sealed class Coin : MonoBehaviour, IPointerClickHandler, IInteractiveValue, IFinishingAction
    {
        public event Action<int> Collected;
        public event Action FinishingActionDone;


        [SerializeField] private TextMeshProUGUI valueMultiplierTextField;
        [SerializeField] private byte minValue;
        [SerializeField] private byte maxValue;

        private int _coinValue;
        private static readonly int Play = Animator.StringToHash("play");

        private int ValueView
        {
            set => valueMultiplierTextField.text = $"x{value.ToString()}";
        }

        private void RefreshValueView()
        {
            ValueView = _coinValue;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnCollected(_coinValue);
            MakeFinishingAction();
        }

        private void MakeFinishingAction()
        {
            SwitchPlayTrigger();
        }

        private void SwitchPlayTrigger()
        {
            var animator = GetComponent<Animator>();
            animator.SetTrigger(Play);
        }

        private void OnCollected(int obj)
        {
            Collected?.Invoke(obj);
        }

        private void GenerateCoinValue()
        {
            _coinValue = SimpleValueGenerator.GenerateIntValueInclusive(minValue, maxValue);
        }

        public void GenerateValue()
        {
            GenerateCoinValue();
            RefreshValueView();
        }

        public void OnFinishingActionDone()
        {
            SwitchPlayTrigger();
            FinishingActionDone?.Invoke();
        }
    }
}