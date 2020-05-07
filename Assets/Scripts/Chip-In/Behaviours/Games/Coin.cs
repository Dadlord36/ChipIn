using System;
using Behaviours.Games.Interfaces;
using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Behaviours.Games
{
    public interface ILockable
    {
        void Lock();
        void Unlock();
    }

    public interface ICollectable
    {
       event Action<IInteractiveUintValue> WasCollected;
    }
    
    public sealed class Coin : MonoBehaviour, IPointerClickHandler, IInteractiveUintValue, IFinishingAction, IResettable,
        ILockable,ICollectable
    {
        public event Action<IInteractiveUintValue> WasCollected;
        public event Action FinishingActionDone;

        [SerializeField] private TextMeshProUGUI valueMultiplierTextField;
        

        private uint _coinValue;
        private static readonly int Play = Animator.StringToHash("play");
        private bool _wasPicked;
        private static readonly int Idle = Animator.StringToHash("idle");

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
            OnWasCollected();
            MakeFinishingAction();
            Lock();
        }

        public void Lock()
        {
            _wasPicked = true;
        }

        public void Unlock()
        {
            _wasPicked = false;
        }

        private void MakeFinishingAction()
        {
            SwitchPlayTrigger();
        }

        private void SwitchPlayTrigger()
        {
            var animator = GetComponent<Animator>();
            animator.SetTrigger(Play);
            animator.ResetTrigger(Idle);
        }

        private void ResetAnimation()
        {
            var animator = GetComponent<Animator>();
            animator.SetTrigger(Idle);
            animator.ResetTrigger(Play);
        }
        

        public void SetValue(uint value)
        {
            _coinValue = value;
            RefreshValueView();
        }

        public void OnFinishingActionDone()
        {
            SwitchPlayTrigger();
            FinishingActionDone?.Invoke();
        }

        public void Reset()
        {
            _wasPicked = false;
            ResetAnimation();
        }


        private void OnWasCollected()
        {
            WasCollected?.Invoke(this);
        }
    }
}