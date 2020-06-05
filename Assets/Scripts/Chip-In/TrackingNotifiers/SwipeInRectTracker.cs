using System;
using ActionsTranslators;
using InputDetection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TrackingNotifiers
{
    [Serializable]
    public sealed class SwipeInRectTracker
    {
        [HideInInspector] public event Action<MoveDirection> SwipedInRect;

        [SerializeField] private RectTransform controlRectTransform;
        [SerializeField] private MainInputActionsTranslator inputActionsTranslator;

        public SwipeInRectTracker(RectTransform controlRectTransform, MainInputActionsTranslator inputActionsTranslator)
        {
            this.controlRectTransform = controlRectTransform;
            this.inputActionsTranslator = inputActionsTranslator;
        }

        public void SubscribeOnInputTranslatorEvents()
        {
            inputActionsTranslator.Swiped += InputActionsTranslatorOnSwiped;
        }

        public void UnsubscribeFromInputTranslatorEvents()
        {
            inputActionsTranslator.Swiped -= InputActionsTranslatorOnSwiped;
        }

        private void InputActionsTranslatorOnSwiped(SwipeDetector.SwipeData swipeData)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(controlRectTransform, swipeData.TouchDownPoint)) return;
            OnSwipedInRect(swipeData.Direction);
            Debug.Log("Swipe was in rect");
        }

        private void OnSwipedInRect(MoveDirection moveDirection)
        {
            SwipedInRect?.Invoke(moveDirection);
        }
    }
}