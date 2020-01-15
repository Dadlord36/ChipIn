using System;
using Behaviours.Games.Interfaces;
using Common.ValueGenerators;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Behaviours.Games
{
    public sealed class Coin : MonoBehaviour, IPointerClickHandler, IInteractiveUintValue, IFinishingAction
    {
        public event Action<uint> Collected;
        public event Action FinishingActionDone;


        [SerializeField] private TextMeshProUGUI valueMultiplierTextField;
        [SerializeField] private byte minValue;
        [SerializeField] private byte maxValue;

        private uint _coinValue;
        private static readonly int Play = Animator.StringToHash("play");
        private bool _wasPicked;

        private uint ValueView
        {
            set => valueMultiplierTextField.text = $"x{value.ToString()}";
        }

        private void RefreshValueView()
        {
            ValueView = _coinValue;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_wasPicked) return;
            OnCollected(_coinValue);
            MakeFinishingAction();
            _wasPicked = true;
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

        private void OnCollected(uint obj)
        {
            Collected?.Invoke(obj);
        }

        private void GenerateCoinValue()
        {
            _coinValue = (uint) SimpleValueGenerator.GenerateIntValueInclusive(minValue, maxValue);
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