using System;
using System.Threading.Tasks;
using Behaviours.Games.Interfaces;
using Common;
using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

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
        ILockable, ICollectable
    {
        [SerializeField] private Graphic graphic;
        [SerializeField] private TextMeshProUGUI valueMultiplierTextField;
        [SerializeField] private int fadingTimeMilliseconds = 1000;
        public event Action<IInteractiveUintValue> WasCollected;
        public event Action FinishingActionDone;


        private Timer _timer;

        private uint _coinValue;
        private bool _wasPicked;

        private uint ValueView
        {
            set => valueMultiplierTextField.text = $"x{value.ToString()}";
        }

        private void OnEnable()
        {
            if (_timer != null) return;
            _timer = new Timer(fadingTimeMilliseconds, false);
            _timer.OnElapsed += OnFinishingActionDone;
        }

        private void RefreshValueView()
        {
            ValueView = _coinValue;
        }

        async void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_wasPicked) return;
            OnWasCollected();
            Lock();
            await MakeFinishingAction();
        }

        public void Lock()
        {
            _wasPicked = true;
        }

        public void Unlock()
        {
            _wasPicked = false;
        }

        private Task MakeFinishingAction()
        {
            return PlayAnimationFromStart();
        }

        private Task PlayAnimationFromStart()
        {
            graphic.CrossFadeAlpha(0f, (float) TimeSpanUtility.ConvertMillisecondsToSeconds(fadingTimeMilliseconds), false);
            return _timer.StartTimer();
        }

        private void ResetAnimation()
        {
            _timer.StopTimer();
        }


        public void SetValue(uint value)
        {
            _coinValue = value;
            RefreshValueView();
        }

        private void OnFinishingActionDone()
        {
            PlayAnimationFromStart();
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